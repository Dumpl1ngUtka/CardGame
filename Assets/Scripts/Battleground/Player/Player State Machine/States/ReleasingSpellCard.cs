using Units;
using UnityEngine;

namespace Battleground
{
    public class ReleasingSpellCard : PlayerState
    {
        private Spell _spell;
        private Piece _piece;

        public ReleasingSpellCard(PlayerStateMachine stateMachine, Piece piece, Spell spell) : base(stateMachine)
        {
            _piece = piece;
            _spell = Object.Instantiate(spell);
        }

        public override LayerMask LayerMask => ~0;

        public override void Enter()
        {
            base.Enter();
            _spell.Init(_piece);
            _spell.StartRelease();
            StateMachine.UI.ShowInfo(_spell);
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.Escape) || _spell.IsSpellReady)
                StateMachine.ChangeState(new SelectSpellCard(StateMachine, _piece));

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
            //_spell.LeftMouseClick(hit);
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            //_spell.RightMouseClick(hit);
        }
    }

}