using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Battleground
{
    public class PieceAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PieceMover _pieceMover;
        [SerializeField] private AnimationClip _idleClip;
        [SerializeField] private AnimationClip _walkClip;
        [SerializeField] private AnimationClip _rotateClip;
        [SerializeField] private AnimationCurve _blendCurve;
        [Header("Value")]
        [SerializeField] private float _locomotionSpeedMult = 1;
        private PlayableGraph _playableGraph;
        private AnimationMixerPlayable _locomotionMixer;
        private AnimationMixerPlayable _topLevelMixer;

        private AnimationClipPlayable _oneShotPlayable;
        private float _oneShotTimer = 0f;
        private float _oneShotAnimationTime;

        public void Init()
        {
            _playableGraph = PlayableGraph.Create("PieceAnimationSystem");

            AnimationPlayableOutput output = AnimationPlayableOutput.Create(_playableGraph, "Animation", _animator);
            _topLevelMixer = AnimationMixerPlayable.Create(_playableGraph, 2);
            output.SetSourcePlayable(_topLevelMixer);

            _locomotionMixer = AnimationMixerPlayable.Create(_playableGraph, 3);
            _topLevelMixer.ConnectInput(0, _locomotionMixer, 0);
            _playableGraph.GetRootPlayable(0).SetInputWeight(0, 1f);

            AnimationClipPlayable idlePlayable = AnimationClipPlayable.Create(_playableGraph, _idleClip);
            AnimationClipPlayable walkPlayable = AnimationClipPlayable.Create(_playableGraph, _walkClip);
            AnimationClipPlayable rotatePlayable = AnimationClipPlayable.Create(_playableGraph, _rotateClip);

            _locomotionMixer.ConnectInput(0, idlePlayable, 0);
            _locomotionMixer.ConnectInput(1, walkPlayable, 0);
            _locomotionMixer.ConnectInput(2, rotatePlayable, 0);

            _playableGraph.Play();
        }

        private void Update()
        {
            UpdateLocomotion(_pieceMover.SpeedFraction, _pieceMover.RotationFraction);
            if (_oneShotPlayable.IsValid())
            {
                if (_oneShotTimer <= 0f)
                    InterruptOneShot();
                else
                    _oneShotTimer -= Time.deltaTime;

                BlendInputs();
            }
        }

        private void BlendInputs()
        {
            var oneShotWeight = _blendCurve.Evaluate(_oneShotTimer / _oneShotAnimationTime);
            _topLevelMixer.SetInputWeight(0, 1 - oneShotWeight);
            _topLevelMixer.SetInputWeight(1, oneShotWeight);
        }

        private void UpdateLocomotion(float speedFraction, float rotationFraction)
        {
            _locomotionMixer.GetInput(1).SetSpeed(speedFraction * _locomotionSpeedMult);
            _locomotionMixer.GetInput(2).SetSpeed(rotationFraction * _locomotionSpeedMult);

            _locomotionMixer.SetInputWeight(0, (1 - speedFraction) * (1 - rotationFraction));
            _locomotionMixer.SetInputWeight(1, speedFraction);
            _locomotionMixer.SetInputWeight(2, rotationFraction * (1 - speedFraction));
            Debug.Log(speedFraction + " + " + rotationFraction);
        }

        public void PlayOneShotAnimation(AnimationClip animationClip, float animationTime)
        {
            if (_oneShotPlayable.IsValid() && _oneShotPlayable.GetAnimationClip() == animationClip)
                return;

            InterruptOneShot();
            _oneShotTimer = animationTime;
            _oneShotAnimationTime = animationTime;
            _oneShotPlayable = AnimationClipPlayable.Create(_playableGraph, animationClip);
            _oneShotPlayable.SetSpeed(animationClip.length / animationTime);
            _topLevelMixer.ConnectInput(1, _oneShotPlayable, 0);
        }


        private void InterruptOneShot()
        {
            _topLevelMixer.SetInputWeight(0, 1f);
            _topLevelMixer.SetInputWeight(1, 0f);

            if (_oneShotPlayable.IsValid())
                DisconnectOneShotInput();
        }

        private void DisconnectOneShotInput()
        {
            _topLevelMixer.DisconnectInput(1);
            _playableGraph.DestroyPlayable(_oneShotPlayable);
        }

        private void OnDestroy()
        {
            if (_playableGraph.IsValid())
                _playableGraph.Destroy();
        }
    }
}