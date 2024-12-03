using Battleground.UI;
using System;
using UnityEngine;

namespace Battleground
{
    public class PlayerStateMachine
    {
        private PlayerState _currentState;
        public BattleSceneUI UI { get; private set; }
        public Player Player { get; private set; }
        public CameraModeChanger CameraMode { get; private set; }

        public PlayerStateMachine(Player player, BattleSceneUI uI, CameraModeChanger cameraMover)
        {
            Player = player;
            UI = uI;
            ChangeState(new SelectCard(this));
            CameraMode = cameraMover;
        }

        public void Update()
        {
            _currentState.Update();
        }

        public virtual void ChangeState(PlayerState state)
        {
            _currentState?.Exit();
            _currentState = state;
            state.Enter();
        }

    }
}
