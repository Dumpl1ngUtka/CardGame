using System;
using System.Collections.Generic;
using UI;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private UnitParameters _parameters;
        private List<GameObject> _activeTabsList = new();
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.UI.Back.performed += CloseOpenTabs;
        }

        private void CloseOpenTabs(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (_activeTabsList.Count == 0)
            {
                OpenMainMenu();
                return;
            }

            var lastTab = _activeTabsList[^1];
            lastTab.gameObject.SetActive(false);
            _activeTabsList.RemoveAt(_activeTabsList.Count - 1);
        }

        private void OpenMainMenu()
        {
            ;
        }

        private void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.UI.Back.performed -= CloseOpenTabs;
        }

        public void ShowUnitInfo(Unit unit)
        {
            _activeTabsList.Add(_parameters.gameObject);
            _parameters.gameObject.SetActive(true);
            _parameters.Init(unit);
        }
    }
}