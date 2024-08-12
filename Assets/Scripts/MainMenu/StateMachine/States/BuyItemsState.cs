using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class BuyItemsState : State
    {
        public override State NextState => throw new System.NotImplementedException();
        
        public BuyItemsState(StateMachine stateMachine) : base(stateMachine)
        {
        }

    }
}

