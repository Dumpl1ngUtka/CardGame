using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCardHolderAnimator : MonoBehaviour
    {
        private Vector2 _screenSize;
        private Animator _animator;
        private string _isSelect = "IsSelect";
        private string _isRerender = "IsRerender";
        private bool _isCardsSelected;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _screenSize = new Vector2(Screen.width, Screen.height);
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

        public void Rerender()
        {
            _animator.SetTrigger(_isRerender);
        }
    }
}
