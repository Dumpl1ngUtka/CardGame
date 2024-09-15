using Battleground.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battleground
{
    public class SelectUnitCard : PlayerState
    {
        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer, CardLayer,UILayer);
        
        public SelectUnitCard(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.UI.ShowInfo(StateMachine.Player,this);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && StateMachine.Player.HasPlayablePiece || StateMachine.Player.IsUnitsListEmpty)
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

        }

        public override void LeftMouseButtonDownOverUI(RaycastResult hit)
        {
            hit.gameObject.TryGetComponent<UICard>(out var card);
            if (card != null)
                StateMachine.ChangeState(new ReleasingUnitCard(StateMachine, card.Unit));
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
        }
    }
}
