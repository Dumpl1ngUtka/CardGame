using System;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Battleground.UI
{
    public class UICard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler
    {
        [SerializeField] private CardRenderer _renderer;
        [SerializeField] private bool _isRealtimeUpdate;
        private CardHolder _cardHolder;
        private RectTransform _rectTransform;
        private Vector3 _targetSize = Vector3.one;
        private Vector3 _targetPosition;
        private float _targetRotation;
        private float _sizeChangeSpeed = 15f;
        private float _lerpSpeed = 5;
        private float _rotatonSpeed = 10f;
        #region SpringMove
        private float _spring = 0.1f;
        private float _drag = 0.3f;
        private Vector2 _vel = Vector2.zero;
        #endregion

        public bool IsSelected { get; private set; } = false;
        public IObjectForUICard ObjectForUICard { get; private set; }
        public RectTransform RectTransform => _rectTransform;

        public void Init(CardHolder cardHolder, IObjectForUICard obj)
        {
            _rectTransform = GetComponent<RectTransform>();
            _cardHolder = cardHolder;
            _renderer.Render(obj);
            ObjectForUICard = obj;
            SetSize(1);
        }

        private void Update()
        {
            if (_isRealtimeUpdate)
            {
                LerpSized(_targetSize);
                LerpMove(_targetPosition);
                LerpRotate(_targetRotation);
            }
        }


        public void SetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        public void SetRotation(float rotation)
        {
            _targetRotation = rotation;
        }

        public void SetSize(float size)
        {
            _targetSize = Vector3.one * size;
        }

        #region Change Transform

        private void LerpMove(Vector3 targetPosition)
        {
            RectTransform.anchoredPosition =
                Vector2.Lerp(RectTransform.localPosition, targetPosition, Time.deltaTime * _lerpSpeed);
        }
        private void LerpSized(Vector3 targetSize)
        {
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, targetSize, Time.deltaTime * _sizeChangeSpeed);
        }

        private void LerpRotate(float targetRotation)
        {
            var newRotation = Quaternion.Euler(0, 0, targetRotation);
            RectTransform.localRotation = Quaternion.Lerp(RectTransform.localRotation, newRotation, Time.deltaTime * _rotatonSpeed);
        }

        private void SpringMove(Vector2 targetPosition)
        {
            _vel += (targetPosition - RectTransform.anchoredPosition) * _spring;
            _vel -= _vel * _drag;
            RectTransform.anchoredPosition += _vel;
        }

        #endregion

        public void OnDrag(PointerEventData eventData)
        {
            _targetPosition = eventData.position;
            _targetPosition.x -= 1920 / 2;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsSelected = true;
            _cardHolder.SelectCardEvent(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //if (_callbackState != null)
            //    _callbackState.LeftMouseButtonDownOverUI(eventData.pointerCurrentRaycast);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsSelected = false;
            _cardHolder.SelectCardEvent(false);
        }
    }
}

