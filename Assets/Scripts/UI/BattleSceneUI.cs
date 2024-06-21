using System;
using System.Collections.Generic;
using UI;
using Units;
using UnityEngine;

namespace Battleground
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private GuildUnitCardRenderer _cardInfo;
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
                OpenMainMenu();

            var lastTab = _activeTabsList[^1];
            lastTab.gameObject.SetActive(false);
            _activeTabsList.RemoveAt(_activeTabsList.Count - 1);
        }

        private void OpenMainMenu()
        {
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
            _playerInput.UI.Back.performed -= CloseOpenTabs;
        }

        public void ShowUnitInfo(Unit unit)
        {
            _activeTabsList.Add(_cardInfo.gameObject);
            _cardInfo.gameObject.SetActive(true);
            _cardInfo.Init(unit);
        }
    }
}