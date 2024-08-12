using Guild;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class SelectUnitsStats : State
    {
        public override State NextState { get => new BuyItemsState(StateMachine); }

        public SelectUnitsStats(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.CameraAnimator.SetTrigger("GameStart");
            StateMachine.CardsAnimator.SetTrigger("GameStart");
            StateMachine.CardsGenerator.GenerateCards();
        }
    }
}