using System.Collections.Generic;
using TMPro;
using Units;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Battleground.UI
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] private UICard _cardPrefab;
        private Vector2 _screenSize;
        private bool _isCardsSelected;
        private List<UICard> _cards = new List<UICard>();
        private UICard _selectedCard;
        private IObjectForInfoRenderer[] _renderedObjects;
        private Vector2 _targetPosition;
        private RectTransform _rectTransform;
        private float _lerpSpeed = 10;
        private PlayerState _callbackState;
        private Dictionary<SpellTypes, bool> _filter = new()
        {
            { SpellTypes.Attack, true },
            { SpellTypes.Heal, true },
            { SpellTypes.Defence, true },
            { SpellTypes.Special, true },
            { SpellTypes.Move, true },
        };
        #region SpringMove
        private float _spring = 0.2f;
        private float _drag = 0.3f;
        private Vector3 _vel = Vector3.zero;
        #endregion

        public RectTransform Container;

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
                _targetPosition = new Vector3(0, -100 -_screenSize.y / 2);
                _isCardsSelected = false;
            }
            LerpMove(_targetPosition);
        }


        public void InstantiateCards(IObjectForInfoRenderer[] objects, PlayerState callbackState)
        {
            if (objects == null)
                return;

            _callbackState = callbackState;
            _cards = new List<UICard>();
            _renderedObjects = objects;
            ClearContainer();
            foreach (var renderedObject in _renderedObjects)
            {
                var spellCard = Instantiate(_cardPrefab, Container);
                spellCard.Init(this, callbackState, renderedObject);
                _cards.Add(spellCard);
            }
            UpdateCards();
        }

        private void UpdateCards()
        {
            var selectedCardIndex = -1;
            var visableCards = new List<UICard>();
            var visableCardsIndex = 0;
            for (int i = 0; i < _cards.Count; i++)
            {
                var spell = _cards[i].Spell;
                if (spell != null && !_filter[spell.Type])
                {
                    _cards[i].SetPosition(new Vector2(0, -1000));
                    continue;
                }
                visableCards.Add(_cards[i]);
                
                if (_cards[i].IsSelected)
                {
                    selectedCardIndex = visableCardsIndex;
                }

                visableCardsIndex++;
            }
            if (selectedCardIndex != -1)
            {
                int index;
                var width = Container.rect.width;
                var cardCount = visableCards.Count;
                var distanceBetweenCards = Mathf.Clamp(width / cardCount, 50, 250);
                var offset = (width - ((cardCount - 1) * distanceBetweenCards + 300)) / 2 + 150;
                for (index = 0; index < selectedCardIndex; index++)
                {
                    var xPos = distanceBetweenCards * index - width / 2 + offset;
                    var influence = Mathf.Max(0,(4 - (selectedCardIndex - index)));
                    var delta = Mathf.Min(100, Mathf.Lerp(0, distanceBetweenCards, (float)influence / (4 - 1)));
                    visableCards[index].SetPosition(new Vector2(xPos - delta,0));
                    visableCards[index].SetSize(0.8f);
                }
                for (index += 1; index < visableCards.Count; index++)
                {
                    var xPos = distanceBetweenCards * index - width / 2 + offset;
                    var influence = Mathf.Max(0, (4 - (index - selectedCardIndex)));
                    var delta = Mathf.Min(100, Mathf.Lerp(0, distanceBetweenCards, (float)influence / (4 - 1)));
                    visableCards[index].SetPosition(new Vector2(xPos + delta,0));
                    visableCards[index].SetSize(0.8f);
                }
            }
            else
            {
                var index = 0;
                var width = Container.rect.width;
                var cardCount = visableCards.Count;
                var delta = Mathf.Clamp(width / cardCount, 50, 250);
                var offset = (width - ((cardCount - 1) * delta + 300)) / 2 + 150;
                foreach (var card in visableCards)
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
            _targetPosition = new Vector3(0, -400 - _screenSize.y / 2);
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
            UpdateCards();
        }

        public void SetFilter(SpellTypes type, bool value)
        {
            _filter[type] = value;
            UpdateCards();
        }
    }
}
