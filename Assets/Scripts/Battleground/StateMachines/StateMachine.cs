using UnityEngine;

namespace Battleground
{
    public abstract class StateMachine 
    {
        protected State CurrentState;

        public virtual void ChangeState(State state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            state.Enter();
        }
    }
}