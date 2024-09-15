using Battleground.UI;
using System;
using UnityEngine;

namespace Battleground
{
    public class PlayerStateMachine : StateMachine
    {
        public BattleSceneUI UI { get; private set; }
        public Player Player { get; private set; }

        public CameraMover CameraMover { get; private set; }

        public PlayerStateMachine(Player player, BattleSceneUI uI, CameraMover cameraMover)
        {
            Player = player;
            UI = uI;
            CurrentState = new WaitOtherPlayer(this);
            CameraMover = cameraMover;
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}
