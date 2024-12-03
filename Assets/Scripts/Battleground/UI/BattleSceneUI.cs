using System.Collections.Generic;
using UI.Marker;
using UnityEngine;

namespace Battleground.UI
{
    public class BattleSceneUI : MonoBehaviour  
    {
        [SerializeField] private Marker _markerPrefab;
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

        public Marker InstantiateMarker(MarkerType type, float radius = 2f)
        {
            var marker = Instantiate(_markerPrefab);
            marker.Init(type, radius);
            return marker;
        }

        public Marker InstantiateMarker(MarkerType type, Color color, float radius = 2f)
        {
            var marker = Instantiate(_markerPrefab);
            marker.Init(type, radius);
            marker.SetColor(color);
            return marker;
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }
    }
}