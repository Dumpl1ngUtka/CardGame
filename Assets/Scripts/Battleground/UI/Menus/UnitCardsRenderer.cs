using UnityEngine;

namespace Battleground.UI
{
    public class UnitCardsRenderer : UIMenu
    {
        [SerializeField] private CardHolder _spellCardHolder;

        public Player Player { get; private set; }

        public void Init(Player player)
        {
            Player = player;
        }

        public override void Open()
        {
            _spellCardHolder.ShowCards(Player.Units.ToArray());
        }

        public override void Close()
        {
            _spellCardHolder.HideCards();
        }
    }

}
