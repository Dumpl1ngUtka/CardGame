using System.Collections;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCardHolder : MonoBehaviour
    {
        [SerializeField] private SpellCard _spellCardPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform[] _spawnPositions;
        private string _isSelect = "IsSelect";
        private string _isRerender = "IsRerender";
        private string _isHideCards = "HideCards";
        private Vector2 _screenSize;
        private Animator _animator;
        private bool _isCardsSelected;
        private Spell[] _spells;

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

        public void ShowCards(Spell[] spells)
        {
            gameObject.SetActive(true);
            _spells = spells;
            _isCardsSelected = false;
            _animator.SetBool(_isRerender, true);
        }

        private void RenderCards()
        {
            ClearContainer();

            var index = (_spawnPositions.Length - _spells.Length) / 2;

            foreach (Spell spell in _spells)
            {
                var spellCard = Instantiate(_spellCardPrefab, _spawnPositions[index].position, _spawnPositions[index].rotation, _container);
                spellCard.Init(spell);
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
