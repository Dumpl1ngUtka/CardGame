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

        public static float TimeStep = 0.1f;
        public Action<float> OnValueChanged;

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
                OnValueChanged?.Invoke(_slider.value);
            }
        }

        public void SetTime(float time)
        {
            _slider.value = time / TimeStep;
        }

        private void OnDisable()
        {
            OnValueChanged -= ChangeRenderedText;
        }

        private void ChangeRenderedText(float value)
        {
            var seconds = Mathf.FloorToInt(value / 10);
            var fractionsOfSecond = Mathf.FloorToInt(value % 10);
            _current.text = string.Format("{0:0}.{1:0}", seconds, fractionsOfSecond);
        }

        public void UpdateTimeline(IObjectForCardRenderer obj)
        {
            var piece = obj as Piece;
            if (piece != null)
                _colorIndicator.Render(piece.Activities);
        }


    }
    public interface IMoveByTimeline
    {
        void NextMove();
        void MoveByTimeline(float index);

    }
}

