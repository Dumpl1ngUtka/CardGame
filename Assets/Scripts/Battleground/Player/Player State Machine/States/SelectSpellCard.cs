using UnityEngine;
using Battleground.UI;
using UnityEngine.EventSystems;

namespace Battleground
{
    public class SelectSpellCard : PlayerState
    {
        private Piece _piece;
        public SelectSpellCard(PlayerStateMachine stateMachine, Piece piece) : base(stateMachine)
        {
            _piece = piece;
        }

        public override LayerMask LayerMask => ~0;

        public override void Enter()
        {
            base.Enter();
            StateMachine.CameraMode.SetPivot(_piece);
            StateMachine.UI.ShowInfo(_piece,this);
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
            if (hit.collider.TryGetComponent<Piece>(out var piece))
            {
                _piece = piece;
                StateMachine.CameraMode.SetPivot(_piece);
                StateMachine.UI.UpdateUnitInfo(_piece, this);
                return;
            }

            StateMachine.ChangeState(new Default(StateMachine));
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
        }

        public override void LeftMouseButtonDownOverUI(RaycastResult hit)
        {
            base.LeftMouseButtonDownOverUI(hit);
            if (hit.gameObject.TryGetComponent<UICard>(out var card))
            {
                StateMachine.ChangeState(new ReleasingSpellCard(StateMachine, _piece, card.Spell));
                return;
            }   
        }
    }
}