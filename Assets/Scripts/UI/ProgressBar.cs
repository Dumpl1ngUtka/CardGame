using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        private const int maxCellCount = 10;

        public void Render(int cellCount)
        {
            _bar.fillAmount = (float)cellCount / maxCellCount;
        }
    }
}
