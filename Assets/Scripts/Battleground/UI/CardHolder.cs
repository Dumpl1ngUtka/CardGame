using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Battleground.UI
{
    public class CardHolder : MonoBehaviour
    {
        #region string const
        private const string _isSelect = "IsSelect";
        private const string _isRerender = "IsRerender";
        private const string _isHideCards = "HideCards";
        #endregion

        [SerializeField] private UICard _cardPrefab;
        [SerializeField] private RectTransform _container;
        private Vector2 _screenSize;
        private bool _isCardsSelected;
        private List<UICard> _cards;
        private IObjectForCardRenderer[] _renderedObjects;
        private Vector2 _targetPosition;
        private RectTransform _rectTransform;
        private float _lerpSpeed = 10;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenSize = new Vector2(Screen.width, Screen.height);
            gameObject.SetActive(false);
            _targetPosition = _rectTransform.localPosition;
        }

        private void Update()
        {
            var verticalMouseScreenPosition = Input.mousePosition.y / _screenSize.y;
            if (verticalMouseScreenPosition < 0.2f && !_isCardsSelected)
            {
                _targetPosition = new Vector3(0, 200 - _screenSize.y / 2);
                _isCardsSelected = true;
            }

            if (verticalMouseScreenPosition > 0.3f && _isCardsSelected)
            {
                _targetPosition = new Vector3(0, -_screenSize.y / 2);
                _isCardsSelected = false;
            }
            LerpMove(_targetPosition);
        }

        private void LerpMove(Vector2 targetPosition)
        {
            _rectTransform.localPosition = 
                Vector2.Lerp(_rectTransform.localPosition, targetPosition, Time.deltaTime * _lerpSpeed);
        }

        public void ShowCards(IObjectForCardRenderer[] objects, PlayerState callbackState)
        {
            if (objects == null)
                return;
            gameObject.SetActive(true);
            _renderedObjects = objects;
            ClearContainer();
            var size = _container.rect.width;
            var cardCount = objects.Length;
            var delta = Mathf.Clamp(size / cardCount, 50, 250);
            var index = 0;
            var offset = (size - ((cardCount - 1) * delta + 300)) / 2 + 150;
            foreach (var renderedObject in _renderedObjects)
            {
                var spellCard = Instantiate(_cardPrefab, _container);
                var rect = spellCard.GetComponent<RectTransform>();
                var xPos = delta * index++ - size / 2 + offset;
                var pos = new Vector3(xPos, -Mathf.Abs(xPos / (size / 2) * 100), index);
                rect.anchoredPosition = pos;
                rect.localRotation = Quaternion.Euler(0, 0, -xPos / (size / 2) * 10);
                spellCard.Init(index, this, callbackState, renderedObject);
            }
        }

        private void UpdateCards()
        {
            //var size = _container.rect.width;
            //var cardCount = objects.Length;
            //var delta = Mathf.Clamp(size / cardCount, 50, 250);
            //var index = 0;
            //var offset = (size - ((cardCount - 1) * delta + 300)) / 2 + 150;
            //foreach (var renderedObject in _renderedObjects)
            //{
            //    var spellCard = Instantiate(_cardPrefab, _container);
            //    var rect = spellCard.GetComponent<RectTransform>();
            //    var xPos = delta * index++ - size / 2 + offset;
            //    var pos = new Vector3(xPos, -Mathf.Abs(xPos / (size / 2) * 200), index);
            //    rect.anchoredPosition = pos;
            //    rect.localRotation = Quaternion.Euler(0, 0, -xPos / (size / 2) * 25);
            //    spellCard.Init(index, this, renderedObject);
            //}
        }

        public void HideCards()
        {
            _targetPosition = new Vector3(0, -200 - _screenSize.y / 2);
        }

        private void ClearContainer()
        {
            foreach (Transform child in _container.transform)
                Destroy(child.gameObject);
        }

        public void SelectCardEvent(int cardIndex)
        {

        }
    }
}
