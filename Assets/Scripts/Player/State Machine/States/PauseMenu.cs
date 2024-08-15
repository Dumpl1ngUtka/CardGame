using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class PauseMenu : State
    {
        public PauseMenu(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override LayerMask LayerMask => LayerMask.GetMask(UILayer);

        public override void Enter()
        {
            base.Enter();
            StateMachine.UI.OpenMainMenu();
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StateMachine.UI.CloseOpenTab();
    
            if (StateMachine.UI.IsTabsListEmpty)
                StateMachine.ChangeState(new Default(StateMachine));

            base.Update();
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
