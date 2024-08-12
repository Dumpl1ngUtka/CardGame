using Units;
using UnityEngine;

namespace Battleground
{
    public class UseSpell : State
    {
        private Spell _spell;
        private Unit _unit;
        private Piece _piece;
        public UseSpell(StateMachine stateMachine, Piece piece, Spell spell) : base(stateMachine)
        {
            _piece = piece;
            _spell = spell;
        }

        public UseSpell(StateMachine stateMachine, Piece piece, Unit unit) : base(stateMachine)
        {
            _piece = piece;
            _unit = unit;
        }

        public override LayerMask LayerMask => _spell.Mask;

        public override void Enter()
        {
            base.Enter();
            _spell.Init(_piece);
            _spell.Start();
            StateMachine.UI.ShowSpellInfo(_spell);
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