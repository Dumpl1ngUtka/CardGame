using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class TimelineCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _icon;
        private Spell _spell;

        public void Init(Spell spell)
        {
            _spell = spell;
            _icon.sprite = spell.SpecializationIcon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                _spell.Piece.RemoveActivityByStartIndex(_spell.StartIndex);
        }
    }
}

