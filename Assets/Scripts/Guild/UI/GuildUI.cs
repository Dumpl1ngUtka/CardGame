using TMPro;
using UnityEngine;

namespace Guild
{
    public class GuildUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rerollCount;
        [SerializeField] private PokerCardsGenerator _cardsGenerator;

        private void Awake()
        {
            UpdateRerollText();
        }

        public void RerollButtonClick()
        {
            UpdateRerollText();
        }

        private void UpdateRerollText()
        {
            if (_rerollCount != null)
                _rerollCount.text = _cardsGenerator.RerollCount.ToString();
        }
    }
}