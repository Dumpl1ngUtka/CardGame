using System.Collections.Generic;
using Units;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private List<UnitClass> Classes;
        [SerializeField] private List<UnitRace> Races;
        [SerializeField] private List<Spell> Spells;
        [SerializeField] private List<Item> Items;
        [Header("Players")]
        [SerializeField] private Player Player1;
        [SerializeField] private Player Player2;
        private Player[] _players;
        private float _newCardTimer;
        private const int _playerCount = 1;
        private const float _giveCardDelay = 5f;
        private const int _startCardCount = 5;

        private void Start()
        {
            _players = PlayersInit();
            GetCardsToPlayers(_startCardCount, true);
        }

        private void Update()
        {
            foreach (var player in _players)
                player.StateMachine.Update();

            if (_newCardTimer < 0)
            {
                GetCardsToPlayers(1, false);
                _newCardTimer = _giveCardDelay;
            }
            else
            {
                _newCardTimer -= Time.deltaTime;
            }
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

        private void GetCardsToPlayers(int cardCount, bool isDuckCards)
        {
            foreach (var player in _players)
            {
                player.AddCards(GetCardsToPlayer(player, cardCount, isDuckCards));
            }
        }

        private List<IObjectForUICard> GetCardsToPlayer(Player player, int cardCount, bool isDuckCards)
        {
            var cards = new List<IObjectForUICard>();
            if (isDuckCards)
            {
                for (int i = 0; i < cardCount; i++)
                {
                    var randomClass = Classes[Random.Range(0, Classes.Count)];
                    var randomRace = Races[Random.Range(0, Races.Count)];
                    var spell = ScriptableObject.CreateInstance<InstantiatePiece>();
                    spell.Init(player, new Unit(2, randomRace, randomClass));
                    cards.Add(spell);
                }
            }
            else
            {
                for (int i = 0; i < cardCount; i++)
                {
                    if (true) //Random.Range(0, 2) == 0
                    {
                        var item = Items[Random.Range(0, Items.Count)];
                        cards.Add(item);
                    }
                    else
                    {
                        var spell = Spells[Random.Range(0, Spells.Count)];
                        spell.Init(player);
                        cards.Add(spell);
                    }
                }
            }
            return cards;
        }
    }
}

