using Battleground.UI;
using System;
using UnityEngine;

namespace Battleground
{
    public class PlayerStateMachine : StateMachine
    {
        public BattleSceneUI UI { get; private set; }
        public Player Player { get; private set; }

        public PlayerStateMachine(Player player, BattleSceneUI uI)
        {
            Player = player;
            UI = uI;
            CurrentState = new WaitOtherPlayer(this);
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}
