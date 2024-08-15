using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private UnitInfoRenderer _infoRenderer;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private UseCardMenu _useCardMenu;
        [SerializeField] private UnitCardsRenderer _unitCards;
        [SerializeField] private List<UIMenu> _activeTabsList = new();

        public bool IsTabsListEmpty => _activeTabsList.Count == 0;

        public void CloseOpenTab()
        {
            var lastTab = _activeTabsList[^1];
            lastTab.Close();
            _activeTabsList.RemoveAt(_activeTabsList.Count - 1);
        }

        public void OpenMainMenu()
        {
            _activeTabsList.Add(_pauseMenu);
            _pauseMenu.Open();
        }

        public void ShowInfo(Unit unit)
        {
            _activeTabsList.Add(_infoRenderer);
            _infoRenderer.Init(unit);
            _infoRenderer.Open();
        }

        public void ShowInfo(Spell spell)
        {
            _activeTabsList.Add(_useCardMenu);
            _useCardMenu.Init(spell);
            _useCardMenu.Open();
        }

        public void ShowInfo(Player player)
        {
            _activeTabsList.Add(_useCardMenu);
            _unitCards.Init(player);
            _unitCards.Open();
        }

        public void UpdateUnitInfo(Unit unit)
        {
            _infoRenderer.Init(unit);
            _infoRenderer.Open();
        }
    }
}