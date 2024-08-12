using Battleground.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class StateMachine : MonoBehaviour
    {
        public BattleSceneUI UI;
        private State _currentState;

        public void ChangeState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            state.Enter();
        }

        private void Update()
        {
            _currentState.Update();
        }
    }
}
