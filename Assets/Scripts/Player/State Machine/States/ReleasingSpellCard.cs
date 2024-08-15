using Units;
using UnityEngine;

namespace Battleground
{
    public class ReleasingSpellCard : State
    {
        private Spell _spell;
        private Piece _piece;

        public ReleasingSpellCard(PlayerStateMachine stateMachine, Piece piece, Spell spell) : base(stateMachine)
        {
            _piece = piece;
            _spell = spell;
        }

        public override LayerMask LayerMask => _spell.Mask;

        public override void Enter()
        {
            base.Enter();
            _spell.Init(_piece);
            _spell.Start();
            StateMachine.UI.ShowInfo(_spell);
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.Escape) || _spell.IsSpellFinished)
                StateMachine.ChangeState(new SelectSpellCard(StateMachine, _piece));

            base.Update();
            _spell.Update();
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