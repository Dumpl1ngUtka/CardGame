using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.UI
{
    public class MenuTab : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private const string EnterTrigger = "Enter";
        private const string ExitTrigger = "Exit";

        public void Enter()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(EnterTrigger);
        }

        public void Exit()
        {
            _animator.SetTrigger(ExitTrigger);
        }

        public void DisableTab()
        {
            gameObject.SetActive(false);
        }
    }
}