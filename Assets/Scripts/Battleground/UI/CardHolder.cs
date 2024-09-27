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
        private Vector2 _screenSize;
        private bool _isCardsSelected;
        private List<UICard> _cards = new List<UICard>();
        private UICard _selectedCard;
        private IObjectForInfoRenderer[] _renderedObjects;
        private Vector2 _targetPosition;
        private RectTransform _rectTransform;
        private float _lerpSpeed = 10;
        #region SpringMove
        private float _spring = 0.2f;
        private float _drag = 0.3f;
        private Vector3 _vel = Vector3.zero;
        #endregion

        public RectTransform Container;
        public RectTransform SelectedCardConteiner;

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
            var width = Container.rect.width;
            var cardCount = objects.Length;
            var delta = Mathf.Clamp(width / cardCount, 50, 250);
            var index = 0;
            var offset = (width - ((cardCount - 1) * delta + 300)) / 2 + 150;
            foreach (var renderedObject in _renderedObjects)
            {
                var spellCard = Instantiate(_cardPrefab, Container);
                spellCard.Init(this, callbackState, renderedObject);
                _cards.Add(spellCard);
                var rect = spellCard.RectTransform;
                var xPos = delta * index++ - width / 2 + offset;
                var pos = new Vector2(xPos, 0);
                spellCard.SetPosition(pos);
                //spellCard.SetRotation(Quaternion.Euler(0, 0, -xPos / (width / 2) * 10));
            }
            _selectedCard = Instantiate(_cardPrefab, Container);
            _selectedCard.gameObject.SetActive(false);
        }

        private void UpdateCards(bool hasSelectedCard)
        {
            var selectedCardIndex = -1;
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].IsSelected)
                {
                    selectedCardIndex = i;
                   // _cards[i].gameObject.SetActive(false);
                    ///_selectedCard.Init(this, _cards[i].CallbackState, _cards[i].Spell != null ? _cards[i].Spell : _cards[i].Unit);
                }
            }
            if (selectedCardIndex != -1)
            {
                //_selectedCard.gameObject.SetActive(true);
                var index = 0;
                var width = Container.rect.width;
                var cardCount = _cards.Count;
                var distanceBetweenCards = Mathf.Clamp(width / cardCount, 50, 250);
                var offset = (width - ((cardCount - 1) * distanceBetweenCards + 300)) / 2 + 150;
                for (index = 0; index < selectedCardIndex; index++)
                {
                    var xPos = distanceBetweenCards * index - width / 2 + offset;
                    var influence = Mathf.Max(0,(4 - (selectedCardIndex - index)));
                    var delta = Mathf.Min(100, Mathf.Lerp(0, distanceBetweenCards, (float)influence / (4 - 1)));
                    _cards[index].SetPosition(new Vector2(xPos - delta,0));
                    _cards[index].SetSize(0.8f);
                }
                //_selectedCard.RectTransform.anchoredPosition = _cards[index].RectTransform.anchoredPosition;
                _selectedCard.SetSize(1.2f);   
                for (index += 1; index < _cards.Count; index++)
                {
                    var xPos = distanceBetweenCards * index - width / 2 + offset;
                    var influence = Mathf.Max(0, (4 - (index - selectedCardIndex)));
                    var delta = Mathf.Min(100, Mathf.Lerp(0, distanceBetweenCards, (float)influence / (4 - 1)));
                    _cards[index].SetPosition(new Vector2(xPos + delta,0));
                    _cards[index].SetSize(0.8f);
                }
            }
            else
            {
                //_selectedCard.gameObject.SetActive(false);
                var index = 0;
                var width = Container.rect.width;
                var cardCount = _cards.Count;
                var delta = Mathf.Clamp(width / cardCount, 50, 250);
                var offset = (width - ((cardCount - 1) * delta + 300)) / 2 + 150;
                foreach (var card in _cards)
                {
                    var xPos = delta * index++ - width / 2 + offset;
                    var pos = new Vector2(xPos, 0);
                    card.SetPosition(pos);
                    card.SetSize(1f);
                }
            }
        }

        public void HideCards()
        {
            _targetPosition = new Vector3(0, -200 - _screenSize.y / 2);
            _isCardsSelected = false;
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
            foreach (Transform child in Container.transform)
                Destroy(child.gameObject);
        }

        public void SelectCardEvent(bool isSelect)
        {
            UpdateCards(isSelect);
        }

        public void SetFilter(ref bool param, bool value)
        {
            param = value;
        }
    }
}
