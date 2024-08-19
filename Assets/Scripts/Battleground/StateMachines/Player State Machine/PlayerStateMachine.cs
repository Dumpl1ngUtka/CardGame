using Battleground.UI;
using System;

namespace Battleground
{
    public class PlayerStateMachine : StateMachine
    {
        public BattleSceneUI UI { get; private set; }
        public Player Player { get; private set; }
        public Piece ReleasingPiece;
        public event Action MoveFinished;

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

        public void StartNewMove()
        {
            ReleasingPiece = null;
        }

        public void MoveFinishedTrigger()
        {
            MoveFinished?.Invoke();
        }
    }
}
