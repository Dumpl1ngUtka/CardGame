using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu.UI
{
    public class MenuTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private MenuPage _thisPage;
        [SerializeField] private MenuPage _nextPage;
        [SerializeField] private Image _colorImage;
        [SerializeField] private Image _blackWhiteImage;
        [SerializeField] private RectTransform _rectTransform;
        private float _colorImageFraction = 0f;
        private float _targetColorImageFraction = 0f;
        private const float _colorChangeSpeed = 10;

        private void Update()
        {
            if (Mathf.Abs(_colorImageFraction - _targetColorImageFraction) > 0.05f)
            {
                _colorImageFraction = Mathf.Lerp(_colorImageFraction, _targetColorImageFraction, Time.deltaTime * _colorChangeSpeed);
                _colorImage.color = new Color(1,1,1,_colorImageFraction);
                _colorImage.rectTransform.localScale = Vector3.one * (1 + _colorImageFraction / 10);
                _blackWhiteImage.rectTransform.localScale = Vector3.one * (1 + _colorImageFraction / 10);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _mainMenuUI.ChangeOpenTab(_thisPage, _nextPage);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _targetColorImageFraction = 1f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _targetColorImageFraction = 0f;
        }

        public void SetRotation(float value)
        {
            _rectTransform.localEulerAngles = new Vector3(0, value, 0);
        }
    }
}