using Battleground.UI;
using System.Collections;
using UnityEngine;

namespace Battleground
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleSceneUI UI;
        [SerializeField] private Player Player;
        [SerializeField] private AIStateMachine AI;
        private const int _playerCount = 2;
        private PlayerStateMachine[] _players;
        private int _currentPlayerIndex = 0;
        private PlayerStateMachine _currentPlayer => _players[_currentPlayerIndex];

        private void Start()
        {
            _players = PlayersInit();
            _currentPlayer.ChangeState(new SelectUnitCard(_currentPlayer));
        }

        private void Update()
        {
            _currentPlayer?.Update();
        }

        private PlayerStateMachine[] PlayersInit()
        {
            var players = new PlayerStateMachine[_playerCount];
            //for (int i = 0; i < _playerCount; i++)
            //{
            //    players[i] = new PlayerStateMachine(Player, UI);
            //    players[i].MoveFinished += NextMove;
            //}
            //players[0] = new PlayerStateMachine(AI, UI);
            players[0] = new PlayerStateMachine(Player, UI);
            players[1] = new PlayerStateMachine(Player, UI);
            return players;
        }

        private void NextMove()
        {
            SetNextPlayer();
            foreach (var player in _players)
            {
                player.StartNewMove();
            }
        }

        private void SetNextPlayer()
        {
            if (++_currentPlayerIndex == _playerCount)
                _currentPlayerIndex = 0;
            _currentPlayer.ChangeState(new SelectUnitCard(_currentPlayer));
        }
    }
}

