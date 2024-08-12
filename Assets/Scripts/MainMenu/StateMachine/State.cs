using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public abstract class State
    {
        public abstract State NextState { get;}
        protected StateMachine StateMachine;

        public State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void Exit() { }
    }

}