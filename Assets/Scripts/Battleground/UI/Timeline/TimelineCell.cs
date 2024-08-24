using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class TimelineCell : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Render(Spell spell)
        {
            _icon.sprite = spell.SpecializationIcon;
        }
    }
}

