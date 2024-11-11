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
        private PlayableGraph _playableGraph;
        private AnimationMixerPlayable _locomotionMixer;
        private AnimationMixerPlayable _topLevelMixer;

        private AnimationClipPlayable _oneShotPlayable;

        public void Init()
        {
            _playableGraph = PlayableGraph.Create("AnimationSystem");

            AnimationPlayableOutput output = AnimationPlayableOutput.Create(_playableGraph, "Animation", _animator);
            _topLevelMixer = AnimationMixerPlayable.Create(_playableGraph, 2);
            output.SetSourcePlayable(_topLevelMixer);

            _locomotionMixer = AnimationMixerPlayable.Create(_playableGraph, 3);
            _topLevelMixer.ConnectInput(0, _locomotionMixer, 0);
            _playableGraph.GetRootPlayable(0).SetInputWeight(0, 1f);

            AnimationClipPlayable idlePlayable = AnimationClipPlayable.Create(_playableGraph, _idleClip);
            AnimationClipPlayable walkPlayable = AnimationClipPlayable.Create(_playableGraph, _walkClip);
            AnimationClipPlayable rotatePlayable = AnimationClipPlayable.Create(_playableGraph, _rotateClip);

            idlePlayable.GetAnimationClip().wrapMode = WrapMode.Loop;
            walkPlayable.GetAnimationClip().wrapMode = WrapMode.Loop;
            walkPlayable.SetSpeed(5);
            rotatePlayable.GetAnimationClip().wrapMode = WrapMode.Loop;

            _locomotionMixer.ConnectInput(0, idlePlayable, 0);
            _locomotionMixer.ConnectInput(1, walkPlayable, 0);
            _locomotionMixer.ConnectInput(2, rotatePlayable, 0);

            _playableGraph.Play();
        }

        private void Update()
        {
            UpdateLocomotion(_pieceMover.SpeedFraction, _pieceMover.RotationFraction);
            if (_oneShotPlayable.IsValid() && _oneShotPlayable.IsDone())
            {
                InterruptOneShot();
            }
        }

        private void UpdateLocomotion(float speedFraction, float rotationFraction)
        {
            _locomotionMixer.SetInputWeight(0, 1f - speedFraction - rotationFraction);
            _locomotionMixer.SetInputWeight(1, speedFraction - rotationFraction);
            _locomotionMixer.SetInputWeight(2, rotationFraction - speedFraction);
        }

        public void PlayOneShotAnimation(AnimationClip animationClip)
        {
            if (_oneShotPlayable.IsValid() && _oneShotPlayable.GetAnimationClip() == animationClip)
                return;

            InterruptOneShot();
            _oneShotPlayable = AnimationClipPlayable.Create(_playableGraph, animationClip);
            _topLevelMixer.ConnectInput(1, _oneShotPlayable, 0);
            _topLevelMixer.SetInputWeight(1, 1f);

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