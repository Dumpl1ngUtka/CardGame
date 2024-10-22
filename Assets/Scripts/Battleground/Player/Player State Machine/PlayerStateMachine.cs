using Battleground.UI;
using System;
using UnityEngine;

namespace Battleground
{
    public class PlayerStateMachine : StateMachine
    {
        public BattleSceneUI UI { get; private set; }
        public Player Player { get; private set; }

        public CameraModeChanger CameraMode { get; private set; }

        public PlayerStateMachine(Player player, BattleSceneUI uI, CameraModeChanger cameraMover)
        {
            Player = player;
            UI = uI;
            ChangeState(new SelectUnitCard(this));
            CameraMode = cameraMover;
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}
