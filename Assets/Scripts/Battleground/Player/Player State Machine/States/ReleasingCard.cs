using Units;
using UnityEngine;

namespace Battleground
{
    public class ReleasingCard : PlayerState
    {
        private Spell _spell;

        public ReleasingCard(PlayerStateMachine stateMachine, Spell spell) : base(stateMachine)
        {
            _spell = spell;
        }

        public override LayerMask LayerMask => ~0;

        public override void Enter()
        {
            base.Enter();
            _spell.Init(StateMachine.Player);
            _spell.StartRelease();
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.Escape) || _spell.IsSpellReleased)
                StateMachine.ChangeState(new SelectCard(StateMachine));

            _spell.Update();
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