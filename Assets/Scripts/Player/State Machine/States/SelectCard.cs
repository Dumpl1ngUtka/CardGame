using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Battleground.UI;

namespace Battleground
{
    public class SelectCard : State
    {
        private Piece _piece;
        public SelectCard(StateMachine stateMachine, Piece piece) : base(stateMachine)
        {
            _piece = piece;
        }

        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer, CardLayer);

        public override void Enter()
        {
            base.Enter();
            StateMachine.UI.ShowUnitInfo(_piece.Unit);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StateMachine.ChangeState(new Default(StateMachine));

            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            StateMachine.UI.CloseOpenTab();
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
            hit.collider.TryGetComponent<Piece>(out var piece);
            if (piece != null)
            {
                _piece = piece;
                StateMachine.UI.UpdateUnitInfo(_piece.Unit);
            }

            hit.collider.TryGetComponent<SpellCard>(out var card);
            if (card != null)
                StateMachine.ChangeState(new UseSpell(StateMachine, _piece, card.Spell));
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }
    }
}