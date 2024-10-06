using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class CardHolderFilterButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CardHolder _cardHolder;
        [SerializeField] private SpellTypes _spellType;
        [SerializeField] private Image _image;
        private bool _isPressed = true;
        private Color _selectedColor = new(1, 1, 1, 1);
        private Color _defaultColor = new(1, 1, 1, 0.5f);

        public void OnPointerClick(PointerEventData eventData)
        {
            _isPressed = !_isPressed;
            _cardHolder.SetFilter(_spellType, _isPressed);
            Render();
        }

        private void Render()
        {
            _image.color = _isPressed? _selectedColor : _defaultColor;
        }
    }
}
