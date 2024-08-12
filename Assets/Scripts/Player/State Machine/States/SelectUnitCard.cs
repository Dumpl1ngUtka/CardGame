using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class SelectUnitCard : State
    {
        public override LayerMask LayerMask => LayerMask.GetMask(PlayerUnitLayer, EnemyUnitLayer, CardLayer);
        
        public SelectUnitCard(StateMachine stateMachine) : base(stateMachine)
        {

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
