using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class WaitOtherPlayer : State
    {
        public override LayerMask LayerMask => LayerMask.GetMask(EnemyUnitLayer);

        public WaitOtherPlayer(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        protected override void LeftMouseButtonDown(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }

        protected override void RightMouseButtonDown(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }
    }
}