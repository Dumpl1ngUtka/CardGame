using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Guild
{
    public class PokerCardsGenerator : CardsGenerator
    {
        [SerializeField] private GuildUnitCard[] _guildUnitCards; 
        private int _rerollCount = 3;

        public int RerollCount
        {
            get { return _rerollCount; }
            set { _rerollCount = value; }
        }

        public override void GenerateCards(bool isReroll = false)  
        {
            if (_rerollCount == 0 && isReroll)
                return;

            bool isAllCardsRerollLocked = isReroll;
            foreach (var card in _guildUnitCards)
                if (!card.IsRerollLock) 
                {
                    isAllCardsRerollLocked = false;
                    break;
                }
            if (isAllCardsRerollLocked)
                return;

            for (int i = 0; i < _guildUnitCards.Length; i++)
            {
                if (_guildUnitCards[i].IsRerollLock && isReroll)
                    continue;
                var unit = GenerateUnit();
                _guildUnitCards[i].Init(unit, !isReroll);
                if (isReroll)
                {
                    _guildUnitCards[i].Reroll();
                }
            }

            if (isReroll)
                _rerollCount--;
        }
    }
}
