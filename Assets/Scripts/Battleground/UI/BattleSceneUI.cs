using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private UnitInfoRenderer _infoRenderer;
        [SerializeField] private GameObject _pauseMenu;
        private List<GameObject> _activeTabsList = new();

        public bool IsTabsListEmpty => _activeTabsList.Count == 0;

        public void CloseOpenTab()
        {
            var lastTab = _activeTabsList[^1];
            lastTab.gameObject.SetActive(false);
            _activeTabsList.RemoveAt(_activeTabsList.Count - 1);
        }

        public void OpenMainMenu()
        {
            _activeTabsList.Add(_pauseMenu);
            _pauseMenu.SetActive(true);
        }

        public void ShowUnitInfo(Unit unit)
        {
            _activeTabsList.Add(_infoRenderer.gameObject);
            _infoRenderer.Init(unit);
            _infoRenderer.gameObject.SetActive(true);
        }

        public void UpdateUnitInfo(Unit unit)
        {
            ShowUnitInfo(unit);
        }
    }
}