using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuState : State
    {
        public override State NextState { get => new SelectUnitsStats(StateMachine);}
        
        public MainMenuState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Exit()
        {
            base.Exit();
            StateMachine.MainMenuAnimator.SetTrigger("GameStart");
        }
    }
}

