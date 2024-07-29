using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private TMP_Text _additionalValue;
        private const int maxValue = 10;

        public void Render(int value, int additionalValue = 0)
        {
            if (_bar != null)
                _bar.fillAmount = (float)value / maxValue;

            if (_value != null)
                _value.text = value.ToString();

            if (_additionalValue != null)
                _additionalValue.text = additionalValue != 0? additionalValue.ToString() : "";
        }
    }
}
