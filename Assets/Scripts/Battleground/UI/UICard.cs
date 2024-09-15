using System;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battleground.UI
{
    public class UICard : MonoBehaviour, IPlayerUI, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private CardRenderer _renderer;
        private IObjectForCardRenderer _containedObj;
        private CardHolder _cardHolder;
        private int _index;
        private PlayerState _callbackState;
        private RectTransform _rectTransform;
        private Vector3 _targetSize = Vector3.one;
        private float _sizeChangeSpeed = 15f;

        public int Weight { get; private set; } = 1;
        public Spell Spell => _containedObj as Spell;
        public Unit Unit => _containedObj as Unit;
        public RectTransform RectTransform => _rectTransform;

        public void Init(int index, CardHolder cardHolder, PlayerState stateForCallback, IObjectForCardRenderer obj)
        {
            _rectTransform = GetComponent<RectTransform>();
            _callbackState = stateForCallback;
            _cardHolder = cardHolder;
            _containedObj = obj;
            if (Spell != null)
                _renderer.Render(Spell);
            else if (Unit != null)
                _renderer.Render(Unit);
            _index = index;
        }

        private void Update()
        {
            LerpSized(_targetSize);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_callbackState != null)
                _callbackState.LeftMouseButtonDownOverUI(eventData.pointerCurrentRaycast); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _targetSize = Vector3.one * 1.2f;
            Weight = 3;
            _cardHolder.SelectCardEvent(_index);
        }

        private void LerpSized(Vector3 targetSize)
        {
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, targetSize, Time.deltaTime * _sizeChangeSpeed);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _targetSize = Vector3.one;
            Weight = 1;
        }
    }
}

