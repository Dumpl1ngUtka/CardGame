using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public class CardHolder : MonoBehaviour, ICardHolder
    {
        [SerializeField] private UICard _cardPrefab;
        [SerializeField] private UICard _selectedCardObject;
        private BattleSceneUI _battleSceneUI;
        private Vector2 _screenSize;
        private List<UICard> _cards = new List<UICard>();
        private List<IObjectForUICard> _renderedObjects;
        private Vector2 _targetPosition;
        private RectTransform _rectTransform;
        private float _lerpSpeed = 10;
        private Dictionary<SpellTypes, bool> _filter = new()
        {
            { SpellTypes.Attack, true },
            { SpellTypes.Heal, true },
            { SpellTypes.Defence, true },
            { SpellTypes.Special, true },
            { SpellTypes.Move, true },
        };
        private UICard _selectedCard;
        #region SpringMove
        private float _spring = 0.2f;
        private float _drag = 0.3f;
        private Vector3 _vel = Vector3.zero;
        #endregion

        private PlayerInput _inputActions => _battleSceneUI.InputActions;
        public RectTransform Container;

        public void Init(BattleSceneUI battleSceneUI)
        {
            battleSceneUI.Player.CardsChanged += a;
            _battleSceneUI = battleSceneUI;
            _rectTransform = GetComponent<RectTransform>();
            _screenSize = new Vector2(Screen.width, Screen.height);
            _targetPosition = _rectTransform.localPosition;
        }

        private void a()
        {

        }

        private void OnEnable()
        {
        }

        private void Update()
        {
            //var verticalMouseScreenPosition = Input.mousePosition.y / _screenSize.y;
            //if (verticalMouseScreenPosition < 0.2f && !_isCardsSelected)
            //{
            //    _targetPosition = new Vector3(0, 200 - _screenSize.y / 2);
            //    _isCardsSelected = true;
            //}

            //if (verticalMouseScreenPosition > 0.3f && _isCardsSelected)
            //{
            //    _targetPosition = new Vector3(0, -100 -_screenSize.y / 2);
            //    _isCardsSelected = false;
            //}

            if (_inputActions.UI.ShowCards.IsPressed())
            {
                _targetPosition = new Vector3(0, 200 - _screenSize.y / 2);
            }
            else
            {
                _targetPosition = new Vector3(0, -100 -_screenSize.y / 2);
            }

            LerpMove(_targetPosition);
        }


        public void InstantiateCards(List<IObjectForUICard> cards)
        {
            if (cards == null)
                return;

            _cards = new List<UICard>();
            ClearContainer();
            foreach (var renderedObject in cards)
            {
                var spellCard = Instantiate(_cardPrefab, Container);
                spellCard.Init(this, renderedObject);
                spellCard.SetPosition(new Vector2(0, 0));
                _cards.Add(spellCard);
            }
            UpdateCards();
        }

        public void AddNewCards(List<IObjectForUICard> cards)
        {
            if (cards == null)
                return;

            foreach (var renderedObject in cards)
            {
                var spellCard = Instantiate(_cardPrefab, Container);
                spellCard.Init(this, renderedObject);
                spellCard.SetPosition(new Vector2(0, -1000));
                _cards.Add(spellCard);
            }
            UpdateCards();
        }

        private void LateUpdate()
        {
            MoveSelectedCard();
        }

        private void UpdateCards()
        {
            var selectedCardIndex = -1;
            var visableCards = new List<UICard>();
            var visableCardsIndex = 0;
            for (int i = 0; i < _cards.Count; i++)
            {
                //if (spell != null && !_filter[spell.Type])
                //{
                //    _cards[i].SetPosition(new Vector2(0, -1000));
                //    continue;
                //}
                visableCards.Add(_cards[i]);
                
                if (_cards[i].IsSelected)
                {
                    selectedCardIndex = visableCardsIndex;
                }

                visableCardsIndex++;
            }
            if (selectedCardIndex != -1)
            {
                _selectedCardObject.gameObject.SetActive(true);
                _selectedCardObject.Init(this, visableCards[selectedCardIndex].ObjectForUICard);
                _selectedCard = visableCards[selectedCardIndex];

                var width = Container.rect.width;
                var cardCount = visableCards.Count;
                var cardWidth = _cardPrefab.GetComponent<RectTransform>().rect.width;
                var distanceBetweenCards = Mathf.Clamp(width / cardCount, 0, cardWidth);
                var offset = width > cardWidth * cardCount ? (width - cardWidth * cardCount + cardWidth) / 2 : distanceBetweenCards / 2;
                var selectedCardPos = distanceBetweenCards * selectedCardIndex - width / 2 + offset;
                var leftCardsCount = selectedCardIndex;
                var rightCardsCount = visableCards.Count - selectedCardIndex - 1;

                visableCards[selectedCardIndex].SetPosition(new Vector2(selectedCardPos, 0));
                visableCards[selectedCardIndex].SetRotation(0);
                visableCards[selectedCardIndex].SetSize(1f);

                if (leftCardsCount > 0)
                {
                    for (int i = 0; i < leftCardsCount; i++)
                    {
                        var xPos = Mathf.Lerp(Container.rect.xMin + offset, selectedCardPos - cardWidth * 0.5f, (float)i / (leftCardsCount));
                        var inContainerPosition = (xPos - Container.rect.xMin) / (Container.rect.xMax - Container.rect.xMin);
                        var rotation = Mathf.Lerp(25, -25, inContainerPosition);
                        var yPos = Mathf.Lerp(0, -100, Mathf.Abs(rotation) / 25);
                        visableCards[i].SetPosition(new Vector2(xPos, yPos));
                        visableCards[i].SetRotation(rotation);
                        visableCards[i].SetSize(0.8f);
                    }
                }
                if (rightCardsCount > 0)
                {
                    for (int i = 0; i < rightCardsCount; i++)
                    {
                        var xPos = Mathf.Lerp(selectedCardPos + cardWidth * 0.5f, Container.rect.xMax - offset, (float)(i + 1) / (rightCardsCount));
                        var inContainerPosition = (xPos - Container.rect.xMin) / (Container.rect.xMax - Container.rect.xMin);
                        var rotation = Mathf.Lerp(25, -25, inContainerPosition);
                        var yPos = Mathf.Lerp(0, -100, Mathf.Abs(rotation) / 25);
                        visableCards[i + selectedCardIndex + 1].SetPosition(new Vector2(xPos, yPos));
                        visableCards[i + selectedCardIndex + 1].SetRotation(rotation);
                        visableCards[i + selectedCardIndex + 1].SetSize(0.8f);
                    }
                }
            }
            else
            {
                _selectedCardObject.gameObject.SetActive(false);
                _selectedCard = null;

                var width = Container.rect.width;
                var cardCount = visableCards.Count;
                var cardWidth = _cardPrefab.GetComponent<RectTransform>().rect.width;
                var distanceBetweenCards = Mathf.Clamp(width / cardCount,0, cardWidth);
                var offset = width > cardWidth * cardCount? (width - cardWidth * cardCount + cardWidth) / 2 : distanceBetweenCards/2;
                for (int i = 0; i < visableCards.Count; i++)
                {
                    var xPos = Mathf.Lerp(Container.rect.xMin + offset, Container.rect.xMax - offset, (float)i / (visableCards.Count - 1));
                    var inContainerPosition = (xPos - Container.rect.xMin) / (Container.rect.xMax - Container.rect.xMin);
                    var rotation = Mathf.Lerp(25, -25, inContainerPosition);
                    var yPos = Mathf.Lerp(0, -100, Mathf.Abs(rotation)/ 25);
                    visableCards[i].SetPosition(new Vector2(xPos, yPos));
                    visableCards[i].SetRotation(rotation);
                    visableCards[i].SetSize(1f);
                }
            }
        }

        private void MoveSelectedCard()
        {
            if (_selectedCard != null)
            {
                _selectedCardObject.RectTransform.localPosition = _selectedCard.RectTransform.localPosition;
                _selectedCardObject.RectTransform.localScale = _selectedCard.RectTransform.localScale;
                _selectedCardObject.RectTransform.localRotation = _selectedCard.RectTransform.localRotation;
            }
        }

        public void HideCards()
        {
            _targetPosition = new Vector3(0, -400 - _screenSize.y / 2);
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
