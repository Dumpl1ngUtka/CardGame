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
        private List<UICard> _cards = new List<UICard>();
        private IObjectForInfoRenderer[] _renderedObjects;
        private Vector2 _targetPosition;
        private RectTransform _rectTransform;
        private float _lerpSpeed = 10;
        #region SpringMove
        private float _spring = 0.2f;
        private float _drag = 0.3f;
        private Vector3 _vel = Vector3.zero;
        #endregion

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenSize = new Vector2(Screen.width, Screen.height);
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


        public void ShowCards(IObjectForInfoRenderer[] objects, PlayerState callbackState)
        {
            if (objects == null)
                return;

            _cards = new List<UICard>();
            _renderedObjects = objects;
            ClearContainer();
            var width = _container.rect.width;
            var cardCount = objects.Length;
            var delta = Mathf.Clamp(width / cardCount, 50, 250);
            var index = 0;
            var offset = (width - ((cardCount - 1) * delta + 300)) / 2 + 150;
            foreach (var renderedObject in _renderedObjects)
            {
                var spellCard = Instantiate(_cardPrefab, _container);
                spellCard.Init(this, callbackState, renderedObject);
                _cards.Add(spellCard);
                var rect = spellCard.RectTransform;
                var xPos = delta * index++ - width / 2 + offset;
                var pos = new Vector3(xPos, -Mathf.Abs(xPos / (width / 2) * 100), index);
                spellCard.SetPosition(pos);
                spellCard.SetRotation(Quaternion.Euler(0, 0, -xPos / (width / 2) * 10));
            }
        }

        private void UpdateCards(bool hasSelectedCard)
        {
            if (hasSelectedCard)
            {
                var index = 0;
                var width = _container.rect.width;
                var aroundSelectedCard= (_cards.Count / 6) + 1 * 2;
                var cardCount = _cards.Count + aroundSelectedCard;
                var delta = Mathf.Clamp(width / cardCount, 25, 150);
                var offset = (width - ((cardCount - 1) * delta + 300)) / 2 + 150;
                for (int i = 0; i < _cards.Count; i++)
                {
                    if (_cards[i].IsSelected)
                    {
                        index += aroundSelectedCard/2;
                        var xPos = delta * index - width / 2 + offset;
                        var pos = new Vector2(xPos, 0);
                        _cards[i].SetPosition(pos);
                        _cards[i].SetRotation(Quaternion.identity);
                        _cards[i].SetSize(1f);
                        index += aroundSelectedCard;
                    }
                    else
                    {
                        var xPos = delta * index++ - width / 2 + offset;
                        var pos = new Vector2(xPos, -Mathf.Abs(xPos / (width / 2) * 50));
                        _cards[i].SetPosition(pos);
                        _cards[i].SetRotation(Quaternion.Euler(0, 0, -xPos / (width / 2) * 10));
                        _cards[i].SetSize(0.8f);

                    }
                }
            }
            else
            {
                var index = 0;
                var width = _container.rect.width;
                var cardCount = _cards.Count;
                var delta = Mathf.Clamp(width / cardCount, 50, 250);
                var offset = (width - ((cardCount - 1) * delta + 300)) / 2 + 150;
                foreach (var card in _cards)
                {
                    var rect = card.RectTransform;
                    var xPos = delta * index++ - width / 2 + offset;
                    var pos = new Vector2(xPos, -Mathf.Abs(xPos / (width / 2) * 50));
                    card.SetPosition(pos);
                    card.SetRotation(Quaternion.Euler(0, 0, -xPos / (width / 2) * 10));
                    card.SetSize(0.8f);
                }
            }
        }

        public void HideCards()
        {
            _targetPosition = new Vector3(0, -200 - _screenSize.y / 2);
        }
        private void LerpMove(Vector3 targetPosition)
        {
            _rectTransform.localPosition = 
                Vector2.Lerp(_rectTransform.localPosition, targetPosition, Time.deltaTime * _lerpSpeed);
        }

        private void SpringMove(Vector3 targetPosition)
        {
            _vel += (targetPosition - _rectTransform.localPosition) * _spring;
            _vel -= _vel * _drag;
            _rectTransform.localPosition += _vel;
        }

        public void ClearContainer()
        {
            foreach (Transform child in _container.transform)
                Destroy(child.gameObject);
        }

        public void SelectCardEvent(bool isSelect)
        {
            UpdateCards(isSelect);
        }
    }
}
