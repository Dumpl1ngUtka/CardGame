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
        private CardHolder _cardHolder;
        private RectTransform _rectTransform;
        private Vector3 _targetSize = Vector3.one;
        private Vector3 _targetPosition;
        private float _targetRotation;
        private float _sizeChangeSpeed = 15f;
        private float _lerpSpeed = 5;
        private float _rotatonSpeed = 10f;
        private bool _isDrag = false;
        private PlayerStateMachine _stateMachine;
        private IObjectForUICard _object;

        public bool IsSelected { get; private set; } = false;
        public IObjectForUICard ObjectForUICard { get; private set; }
        public RectTransform RectTransform => _rectTransform;

        public void Init(PlayerStateMachine stateMachine,CardHolder cardHolder, IObjectForUICard obj)
        {
            _stateMachine = stateMachine;
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
            if (_isRealtimeUpdate && !_isDrag)
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
                _stateMachine.ChangeState(new ReleasingCard(_stateMachine, this));
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
            RectTransform.anchoredPosition = newPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDrag = false;
            _image.raycastTarget = true;

            if (eventData.pointerCurrentRaycast.gameObject == null)
                return;

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<UICard>(out var card))
            {

            };
            //if (card is ICardHolder cardHolder)
        }

        #endregion


    }
}

