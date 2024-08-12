using UnityEngine;

namespace MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {

        private MenuTab _currentTab;

        public void ChangeTab(MenuTab newTab)
        {
            _currentTab?.Exit();
            _currentTab = newTab;
            newTab.Enter();
        }

        private void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }
    }
}
