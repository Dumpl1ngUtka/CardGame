using UnityEngine;
using Battleground.UI;


namespace Battleground
{
    public class SelectSpellCard : PlayerState
    {
        private Piece _piece;
        public SelectSpellCard(PlayerStateMachine stateMachine, Piece piece) : base(stateMachine)
        {
            _piece = piece;
        }

        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer, CardLayer);

        public override void Enter()
        {
            base.Enter();
            StateMachine.UI.ShowInfo(_piece);
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

            hit.collider.TryGetComponent<UICard>(out var card);
            if (card != null)
                StateMachine.ChangeState(new ReleasingSpellCard(StateMachine, _piece, card.Spell));

            hit.collider.TryGetComponent<Piece>(out var piece);
            if (piece == null)
                return;

            if (StateMachine.ReleasingPiece == null || piece == StateMachine.ReleasingPiece)
            {
                _piece = piece;
                StateMachine.UI.UpdateUnitInfo(_piece);
            }
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }
    }
}