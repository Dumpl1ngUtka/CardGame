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
        private float _maxTime;
        private float _minTime;
        private float _oldValue;
        private float _currentTime;

        public Action<float, bool> OnTimeChanged;

        public float GetTime => _currentTime;
        public float MinTime => _minTime;
        public float MaxTime => _maxTime;
        public float TimeRange => _maxTime - _minTime;

        public void SetTimeBounds(float minTime, float maxTime)
        {
            _minTime = minTime;
            _maxTime = maxTime;

            _slider.minValue = minTime;
            _slider.maxValue = maxTime;
            _slider.value = _minTime;
            _oldValue = _slider.value;

            ChangeRenderedText(_slider.value);
        }

        private void OnEnable()
        {
            OnTimeChanged += ChangeRenderedText;
        }

        private void Update()
        {
            if (_slider.value != _oldValue)
            {
                _oldValue = _slider.value;
                SetTime(_slider.value, false);
            }
        }

        public void SetTime(float time, bool isSimulation)
        {
            _currentTime = time;
            OnTimeChanged?.Invoke(_currentTime, isSimulation);
        }

        private void OnDisable()
        {
            OnTimeChanged -= ChangeRenderedText;
        }

        private void ChangeRenderedText(float value, bool isSimulation = false)
        {
            _current.text = string.Format("{0:0.00}", value);
            _max.text = string.Format("{0:0.00}", _maxTime);
        }

        public void UpdateTimelineRender(IObjectForCardRenderer obj)
        {
            var piece = obj as Piece;
            if (piece != null)
                _colorIndicator.Render(piece.Activities);
        }
    }

    public interface IMoveByTimeline
    {
        void NextMove();

        void MoveByTimeline(float time, bool isSimulation = false);

    }
}

