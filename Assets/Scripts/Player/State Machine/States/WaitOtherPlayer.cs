using UnityEngine;

namespace Battleground
{
    public class WaitOtherPlayer : State
    {
        public override LayerMask LayerMask => LayerMask.GetMask(EnemyUnitLayer);

        public WaitOtherPlayer(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.MoveFinishedTrigger();
        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
        }
    }
}