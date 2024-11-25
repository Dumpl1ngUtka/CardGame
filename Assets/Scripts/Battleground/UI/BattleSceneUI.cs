using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private InfoRenderer _infoRenderer;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private List<UIMenu> _activeTabsList = new();
        public bool IsTabsListEmpty => _activeTabsList.Count == 0;
        public PlayerInput InputActions { get; private set; }
        public Player Player { get; private set; }

        public void Init(Player player)
        {
            Player = player;
        }

        private void OnEnable()
        {
            InputActions = new();
            InputActions.Enable();
        }

        public void CloseOpenTab()
        {
            var lastTab = _activeTabsList[^1];
            lastTab.Close();
            _activeTabsList.RemoveAt(_activeTabsList.Count - 1);
        }

        public void OpenPauseMenu()
        {
            _activeTabsList.Add(_pauseMenu);
            _pauseMenu.Open();
        }

        public void ShowInfo(IObjectForInfoRenderer obj)
        {
            _activeTabsList.Add(_infoRenderer);

            _infoRenderer.Init(obj);
            _infoRenderer.Open();
        }

        public void UpdateUnitInfo(IObjectForInfoRenderer obj)
        {
            _infoRenderer.Init(obj);
            _infoRenderer.Open();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }
    }
}