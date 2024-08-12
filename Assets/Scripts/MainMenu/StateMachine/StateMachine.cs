using Battleground.UI;
using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guild;
using MainMenu.UI;

namespace MainMenu
{
    public class StateMachine : MonoBehaviour
    {
        public CardsGenerator CardsGenerator;
        public Animator CardsAnimator;
        public Animator CameraAnimator;
        public Animator MainMenuAnimator;
        public MainMenuUI MainMenuUI;

        private State _currentState;

        private void Awake()
        {
            _currentState = new MainMenuState(this);
        }

        public void ChangeState()
        {
            _currentState.Exit();
            _currentState = _currentState.NextState;
            _currentState.Enter();
        }

        private void Update()
        {
            _currentState.Update();
        }
    }
}

