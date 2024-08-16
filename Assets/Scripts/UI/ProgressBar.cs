using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private TMP_Text _additionalValue;
        private const int maxValue = 10;

        public void Render(int value, int maxValue = maxValue, int additionalValue = 0)
        {
            SetActive(true);
            if (_bar != null)
                _bar.fillAmount = (float)value / maxValue;

            if (_value != null)
                _value.text = value.ToString();

            if (_additionalValue != null)
                _additionalValue.text = additionalValue != 0? additionalValue.ToString() : "";
        }

        public void Render(float fillValue)
        {
            SetActive(true);
            if (_bar != null)
                _bar.fillAmount = fillValue;
        }

        public void SetActive(bool isActive)
        {
            _bar?.gameObject.SetActive(isActive);
            _background?.gameObject.SetActive(isActive);
            _value?.gameObject.SetActive(isActive);
            _additionalValue?.gameObject.SetActive(isActive);
        }
    }
}
