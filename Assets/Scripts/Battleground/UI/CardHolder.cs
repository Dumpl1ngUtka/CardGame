using Units;
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
        [SerializeField] private Transform _container;
        [SerializeField] private Transform[] _spawnPositions;
        private Vector2 _screenSize;
        private Animator _animator;
        private bool _isCardsSelected;
        private IObjectForCardRenderer[] _renderedObjects;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _screenSize = new Vector2(Screen.width, Screen.height);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            var verticalMouseScreenPosition = Input.mousePosition.y / _screenSize.y;
            if (verticalMouseScreenPosition < 0.2f && !_isCardsSelected)
            {
                _isCardsSelected = true;
                _animator.SetBool(_isSelect, true);
            }

            if (verticalMouseScreenPosition > 0.3f && _isCardsSelected)
            {
                _isCardsSelected = false;
                _animator.SetBool(_isSelect, false);
            }
        }

        public void ShowCards(IObjectForCardRenderer[] objects)
        {
            gameObject.SetActive(true);
            _renderedObjects = objects;
            _isCardsSelected = false;
            _animator.SetBool(_isRerender, true);
        }

        private void RenderCards()
        {
            ClearContainer();

            var index = (_spawnPositions.Length - _renderedObjects.Length) / 2;

            foreach (var renderedObject in _renderedObjects)
            {
                var spellCard = Instantiate(_cardPrefab, _spawnPositions[index].position, _spawnPositions[index].rotation, _container);
                spellCard.Init(renderedObject);
                index++;
            }

        }

        public void HideCards()
        {
            _animator.SetTrigger(_isHideCards);
        }

        private void ClearContainer()
        {
            foreach (Transform child in _container.transform)
                Destroy(child.gameObject);
        }
    }
}
