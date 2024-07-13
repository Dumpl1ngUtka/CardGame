using UnityEngine;

namespace Guild
{
    public class GuildCardAnimator : MonoBehaviour
    {
        private Animator _animator;
        private const string _isSelect = "IsSelect";
        private const string _isFlip = "Flip";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.SetBool(_isSelect, false);
        }

        private void OnMouseEnter()
        {
            _animator.SetBool(_isSelect, true);
        }

        public void OnMouseExit()
        {
            _animator.SetBool(_isSelect, false);
        }

        public void RerollAnimation()
        {
            _animator.SetTrigger(_isFlip);
        }
    }
}