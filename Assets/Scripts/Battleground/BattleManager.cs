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
        private const float _moveTime = 10f;

        private void Start()
        {
            _players = PlayersInit();
            SetNextMove();
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
                players[i].MoveFinished += ContestantMoveFinished;
            }
            return players;
        }

        private void ContestantMoveFinished()
        {
            if (IsAllContestantsMoveFinished())
            {
                StartCoroutine(Simulation());
            }

        }

        public IEnumerator Simulation()
        {
            var timer = 0f;
            while (timer < _moveTime)
            {
                foreach(var player in _players)
                    player.Timeline.SetTime(timer);
                timer += Time.deltaTime;
                yield return null;
            }
            SetNextMove();
        }

        private bool IsAllContestantsMoveFinished()
        {
            bool isAllContestantMoveFinished = true;
            foreach (var player in _players)
                if (!player.IsMoveFinished)
                    isAllContestantMoveFinished = false;
            return isAllContestantMoveFinished;
        }

        private void SetNextMove()
        {
            foreach (var player in _players)
                player.StartNewMove();
        }
    }
}

