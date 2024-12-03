using Battleground.UI;
using System.Collections.Generic;
using Units;
using Units.Items;
using UnityEngine;

namespace Battleground
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Cards")]
        [SerializeField] private UICard _defaultCardPrefab;
        [SerializeField] private DuckUICard _duckCardPrefab;
        [Header("Unit")]
        [SerializeField] private InstantiatePiece InstantiateDuckSpell;
        [SerializeField] private List<UnitClass> Classes;
        [SerializeField] private List<UnitRace> Races;
        [SerializeField] private List<Spell> Spells;
        [SerializeField] private List<Item> Items;
        [Header("Players")]
        [SerializeField] private Player Player1;
        [SerializeField] private Player Player2;
        private Player[] _players;
        private float _newCardTimer = _giveCardDelay;
        private const int _playerCount = 1;
        private const float _giveCardDelay = 50f;
        private const int _startCardCount = 7;

        private void Start()
        {
            _players = PlayersInit();
            GetCardsToPlayers(_startCardCount, true);
            GetCardsToPlayers(3, false);
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

        private List<UICard> GetCardsToPlayer(Player player, int cardCount, bool isDuckCards)
        {
            var cards = new List<UICard>();
            if (isDuckCards)
            {
                for (int i = 0; i < cardCount; i++)
                {
                    var randomClass = Classes[Random.Range(0, Classes.Count)];
                    var randomRace = Races[Random.Range(0, Races.Count)];
                    var spell = Instantiate(InstantiateDuckSpell);
                    spell.Init(player, new Unit(1, randomRace, randomClass));

                    var duckCard = Instantiate(_duckCardPrefab);
                    duckCard.Init(player.StateMachine, player.CardHolder, spell);
                    duckCard.SetPosition(new Vector3(0, -1000));
                    cards.Add(duckCard);
                }
            }
            else
            {
                for (int i = 0; i < cardCount; i++)
                {
                    IObjectForUICard item = false? Items[Random.Range(0, Items.Count)] : Spells[Random.Range(0, Spells.Count)];
                    //IObjectForUICard item = Random.Range(0, 2) == 0? Items[Random.Range(0, Items.Count)] : Spells[Random.Range(0, Spells.Count)];
                    var card = Instantiate(_defaultCardPrefab);
                    card.Init(player.StateMachine, player.CardHolder, item);
                    card.SetPosition(new Vector3(0, -1000));
                    cards.Add(card);
                }
            }
            return cards;
        }
    }
}

