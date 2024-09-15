using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private InfoRenderer _infoRenderer;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private List<UIMenu> _activeTabsList = new();
        [SerializeField] private Timeline _timeline;

        public bool IsTabsListEmpty => _activeTabsList.Count == 0;

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

        public void ShowInfo(IObjectForCardRenderer obj, PlayerState callbackState = null)
        {
            _activeTabsList.Add(_infoRenderer);
            _infoRenderer.Init(obj, callbackState);
            _infoRenderer.Open();
            _timeline.UpdateTimelineRender(obj);
        }

        public void UpdateUnitInfo(IObjectForCardRenderer obj, PlayerState callbackState = null)
        {
            _infoRenderer.Init(obj, callbackState);
            _infoRenderer.Open();
            _timeline.UpdateTimelineRender(obj);

        }
    }
}