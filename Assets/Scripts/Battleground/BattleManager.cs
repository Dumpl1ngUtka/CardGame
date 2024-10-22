using Battleground.UI;
using System.Collections;
using UnityEngine;

namespace Battleground
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private Player Player1;
        [SerializeField] private Player Player2;
        private Player[] _players;
        private const int _playerCount = 1;

        private void Start()
        {
            _players = PlayersInit();
        }

        private void Update()
        {
            foreach (var player in _players)
                player.StateMachine.Update();
        }

        private Player[] PlayersInit()
        {
            var players = new Player[_playerCount];
            for (int i = 0; i < _playerCount; i++)
            {
                //переделать 
                if (i == 0)
                    players[i] = Player1;
                else
                    players[i] = Player2;
                //
            }
            return players;
        }
    }
}

