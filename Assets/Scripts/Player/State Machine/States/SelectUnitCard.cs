using Battleground.UI;
using UnityEngine;

namespace Battleground
{
    public class SelectUnitCard : State
    {
        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer, CardLayer);
        
        public SelectUnitCard(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.UI.ShowInfo(StateMachine.Player);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && StateMachine.Player.HasPlayablePiece || StateMachine.Player.IsUnitsListEmpty)
                StateMachine.ChangeState(new WaitOtherPlayer(StateMachine));

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
                StateMachine.ChangeState(new ReleasingUnitCard(StateMachine, card.Unit));
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }
    }
}
