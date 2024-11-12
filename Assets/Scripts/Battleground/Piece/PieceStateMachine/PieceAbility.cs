using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceAbility : PieceState, IObjectForInfoRenderer
    {
        #region Main Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _releaseTime;
        [SerializeField] private float _cooldown;
        [SerializeField] private AnimationClip _animationClip;
        #endregion

        private bool _isReadyToUse = true;
        private readonly List<PieceAbility> _emptyList = new();

        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float ReleaseTime => _releaseTime;
        public float Cooldown => _cooldown;
        public bool IsReadyToUse => _isReadyToUse;

        #endregion

        protected override float MinStateTime => ReleaseTime;
        protected override float MaxStateTime => ReleaseTime;
        protected override List<PieceAbility> AvailableAbilityList => _emptyList;
        public override Transform Target => throw new NotImplementedException();

        protected PieceState PriviousState;

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
            {
                Title = Name,
                UnderTitle = Description,
                ContentLine1 = "Release time = " + ReleaseTime,
                ContentLine2 = "Cooldown time = " + Cooldown,
            };
        }

        public override void Enter(PieceState pieceState)
        {
            base.Enter(pieceState);
            PriviousState = pieceState;
            Piece.Animator.PlayOneShotAnimation(_animationClip, ReleaseTime);
        }

        public IEnumerator Charge()
        {
            var timer = Cooldown;
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                yield return null;
                //Debug.Log(Name + " cooldown " + timer);
            }
            _isReadyToUse = true;
        }

        public override void Exit()
        {
            base.Exit();
            _isReadyToUse = false;
            StateMachine.ChargeAbility(Charge());
        }
    }
}
