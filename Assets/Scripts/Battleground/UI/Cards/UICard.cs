using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battleground.UI
{
    public class UICard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private CardRenderer _renderer;
        [SerializeField] private bool _isRealtimeUpdate;
        private Image _image;
        private ICardHolder _cardHolder;
        private RectTransform _rectTransform;
        private float _sizeChangeSpeed = 15f;
        private float _lerpSpeed = 5;
        private float _rotatonSpeed = 10f;
        private bool _isDrag = false;
        private IObjectForUICard _object;
        protected PlayerStateMachine StateMachine;
        protected Vector3 TargetPosition;
        protected float TargetSize = 1f;
        protected float TargetRotation;

        public ICardHolder CardHolder => _cardHolder;
        public bool IsSelected { get; private set; } = false;
        public IObjectForUICard ObjectForUICard { get; private set; }
        public RectTransform RectTransform => _rectTransform;

        public void Init(PlayerStateMachine stateMachine, ICardHolder cardHolder, IObjectForUICard obj)
        {
            StateMachine = stateMachine;
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            _cardHolder = cardHolder;
            _object = obj;
            _renderer.Render(obj);
            ObjectForUICard = obj;
            SetSize(1);
        }

        private void Update()
        {
            if (!_isRealtimeUpdate || _isDrag)
                return;

            LerpSized(TargetSize);
            LerpMove(TargetPosition);
            LerpRotate(TargetRotation);
        }

        public void SetParent(RectTransform rectTransform, ICardHolder cardHolder)
        {
            RectTransform.SetParent(rectTransform);
            _cardHolder = cardHolder;
        }

        public void SetPosition(Vector3 position)
        {
            TargetPosition = position;
        }

        public void SetRotation(float rotation)
        {
            TargetRotation = rotation;
        }

        public void SetSize(float size)
        {
            TargetSize = size;
        }

        #region Change Transform

        private void LerpMove(Vector3 targetPosition)
        {
            RectTransform.localPosition =
                Vector2.Lerp(RectTransform.localPosition, targetPosition, Time.deltaTime * _lerpSpeed);
        }
        private void LerpSized(float targetSize)
        {
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, Vector3.one * targetSize, Time.deltaTime * _sizeChangeSpeed);
        }

        private void LerpRotate(float targetRotation)
        {
            var newRotation = Quaternion.Euler(0, 0, targetRotation);
            RectTransform.localRotation = Quaternion.Lerp(RectTransform.localRotation, newRotation, Time.deltaTime * _rotatonSpeed);
        }

        #endregion


        public void OnPointerEnter(PointerEventData eventData)
        {
            IsSelected = true;
            _cardHolder.SelectCardEvent();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_object as Spell)
            {
                StateMachine.ChangeState(new ReleasingCard(StateMachine, this));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsSelected = false;
            _cardHolder.SelectCardEvent();
        }

        #region Drag

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDrag = true;
            _image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var newPos = eventData.position;
            newPos.x -= Screen.width / 2;
            newPos.y -= RectTransform.rect.height / 2;
            FollowTo(newPos);
        }

        private void FollowTo(Vector2 posByPixels)
        {
            RectTransform.anchoredPosition = posByPixels;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _isDrag = false;
            _image.raycastTarget = true;

            if (eventData.pointerCurrentRaycast.gameObject == null)
            {
                if (CardHolder is DuckUICard card)
                    card.CardHolder.Add(this);
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<DuckUICard>(out var duckCard))
            {
                duckCard.Add(this);
            }
        }

        #endregion


    }
}

