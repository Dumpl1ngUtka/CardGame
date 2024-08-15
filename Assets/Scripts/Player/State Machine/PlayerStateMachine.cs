using Battleground.UI;
using System;
using UnityEngine;

namespace Battleground
{
    public class PlayerStateMachine
    {
        private State _currentState;

        public BattleSceneUI UI { get; private set; }
        public Player Player { get; private set; }

        public event Action MoveFinished;

        public PlayerStateMachine(Player player, BattleSceneUI uI)
        {
            Player = player;
            UI = uI;
            _currentState = new WaitOtherPlayer(this);
        }

        public void ChangeState(State state)
        {
            Debug.Log("new state" + state);
            _currentState?.Exit();
            _currentState = state;
            state.Enter();
        }

        public void StartNewMove()
        {

        }

        public void Update()
        {
            _currentState.Update();
        }

        public void MoveFinishedTrigger()
        {
            MoveFinished?.Invoke();
        }

        //public void InstantiatePiece()
    }
}
