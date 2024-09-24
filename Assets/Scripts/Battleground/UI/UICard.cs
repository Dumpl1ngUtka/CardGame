using System;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Battleground.UI
{
    public class UICard : MonoBehaviour, IPlayerUI, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private CardRenderer _renderer;
        private IObjectForInfoRenderer _containedObj;
        private CardHolder _cardHolder;
        private PlayerState _callbackState;
        private RectTransform _rectTransform;
        private Vector3 _targetSize = Vector3.one;
        private Vector3 _targetPosition;
        private float _sizeChangeSpeed = 15f;
        private float _lerpSpeed = 10;
        #region SpringMove
        private float _spring = 0.1f;
        private float _drag = 0.3f;
        private Vector2 _vel = Vector2.zero;
        #endregion
        private Quaternion _targetRotation;
        private float _rotatonSpeed = 10;

        public bool IsSelected { get; private set; } = false;
        public Spell Spell => _containedObj as Spell;
        public Unit Unit => _containedObj as Unit;
        public RectTransform RectTransform => _rectTransform;
        public PlayerState CallbackState => _callbackState;

        public void Init(CardHolder cardHolder, PlayerState stateForCallback, IObjectForInfoRenderer obj)
        {
            _rectTransform = GetComponent<RectTransform>();
            _callbackState = stateForCallback;
            _cardHolder = cardHolder;
            _containedObj = obj;
            if (Spell != null)
                _renderer.Render(Spell);
            else if (Unit != null)
                _renderer.Render(Unit);
            SetSize(1);
        }

        private void Update()
        {
            LerpSized(_targetSize);
            LerpMove(_targetPosition);
            //LerpRotate(_targetRotation);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_callbackState != null)
                _callbackState.LeftMouseButtonDownOverUI(eventData.pointerCurrentRaycast); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsSelected = true;
            _cardHolder.SelectCardEvent(true);
        }



        private void LerpSized(Vector3 targetSize)
        {
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, targetSize, Time.deltaTime * _sizeChangeSpeed);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsSelected = false;
            _cardHolder.SelectCardEvent(false);
        }

        public void SetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _targetRotation = rotation;
        }

        private void LerpMove(Vector3 targetPosition)
        {
            RectTransform.anchoredPosition =
                Vector2.Lerp(_rectTransform.localPosition, targetPosition, Time.deltaTime * _lerpSpeed);
        }
        private void SpringMove(Vector2 targetPosition)
        {
            _vel += (targetPosition - RectTransform.anchoredPosition) * _spring;
            _vel -= _vel * _drag;
            RectTransform.anchoredPosition += _vel;
        }

        private void LerpRotate(Quaternion targetRotation)
        {
            RectTransform.localRotation = Quaternion.Lerp(RectTransform.localRotation,targetRotation, Time.deltaTime * _rotatonSpeed);
        }

        public void SetSize(float size)
        {
            _targetSize = Vector3.one * size;
        }
    }
}

