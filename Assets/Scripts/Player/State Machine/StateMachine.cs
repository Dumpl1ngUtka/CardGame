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

        private void Awake()
        {
            _currentState = new Default(this);
        }

        public void ChangeState(State state)
        {
            Debug.Log("State changed to " + state);
            _currentState.Exit();
            _currentState = state;
            state.Enter();
        }

        private void Update()
        {
            _currentState.Update();
        }
    }
}
