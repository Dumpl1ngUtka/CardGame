using Battleground.UI;
using Units;
using UnityEngine;

namespace Battleground
{
    public class ReleasingCard : PlayerState
    {
        private Spell _spell;
        private UICard _card;

        public ReleasingCard(PlayerStateMachine stateMachine, UICard card) : base(stateMachine)
        {
            _card = card;
            
            if (_card.ObjectForUICard is Spell spell)
                _spell = spell;
            else
                StateMachine.ChangeState(new SelectCard(StateMachine));
        }

        public override LayerMask LayerMask 
        {
            get
            {
                if (_spell != null)
                    return _spell.LayerMask;
                return ~0;
            }
        }

        public override void Enter()
        {
            base.Enter();
            _spell.Init(StateMachine.Player);
            _spell.StartRelease();
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                StateMachine.ChangeState(new SelectCard(StateMachine));

            if (_spell.IsSpellReleased)
            {
                StateMachine.Player.RemoveCard(_card);
                StateMachine.ChangeState(new SelectCard(StateMachine));
            }

            _spell.Release(LastHit);
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _spell.EndRelease();
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
            _spell.LeftMouseClick(hit);
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            _spell.RightMouseClick(hit);
        }
    }

}