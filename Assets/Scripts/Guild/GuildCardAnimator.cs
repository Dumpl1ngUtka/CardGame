using UnityEngine;

namespace Guild
{
    public class GuildCardAnimator : MonoBehaviour
    {
        private Animator _animator;
        private const string _isSelect = "IsSelect";
        private const string _isFlip = "IsFlip";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnMouseEnter()
        {
            _animator.SetBool(_isSelect, true);
        }

        public void OnMouseExit()
        {
            _animator.SetBool(_isSelect, false);
        }

        public void FlipAnimation()
        {
            _animator.SetTrigger(_isFlip);
        }
    }
}