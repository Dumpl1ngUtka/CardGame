using UnityEngine;

namespace MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private MenuPage[] _pages;

        public void ChangeOpenTab(MenuPage currentPage, MenuPage nextPage)
        {
            StartCoroutine(currentPage.Close());
            StartCoroutine(nextPage.Open());
        }
    }
}
