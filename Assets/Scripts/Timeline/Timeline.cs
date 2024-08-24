using Battleground.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battleground
{
    public class Timeline : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _current;
        [SerializeField] private TMP_Text _max;
        [SerializeField] private TimelineColorIndicator _colorIndicator;
        private int _maxTimePerSecond = 10;
        private float _oldValue;

        public const float TimeStep = 0.1f;
        public Action<int> OnValueChanged;

        public int GetIndex
        {
            get
            {
                return (int)(_slider.value);
            }
        }
        public int MaxIndex
        {
            get
            {
                return (int)Mathf.Ceil(_maxTimePerSecond / TimeStep);
            }
        }

        private void OnEnable()
        {
            _slider.maxValue = _maxTimePerSecond/ TimeStep;
            _oldValue = _slider.value;
            OnValueChanged += ChangeRenderedText;
            ChangeRenderedText((int)_slider.value);
        }

        private void Update()
        {
            if (_slider.value != _oldValue)
            {
                _oldValue = _slider.value;
                OnValueChanged?.Invoke((int)_slider.value);
            }
        }

        private void OnDisable()
        {
            OnValueChanged -= ChangeRenderedText;
        }

        private void ChangeRenderedText(int newText)
        {
            _current.text = newText.ToString();
        }

        public void UpdateTimeline(IObjectForCardRenderer obj)
        {
            var piece = obj as Piece;
            if (piece != null)
                _colorIndicator.Render(piece.Activities);
        }
    }
}

