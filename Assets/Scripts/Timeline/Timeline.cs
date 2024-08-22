using UnityEngine;
using UnityEngine.UI;

namespace Battleground
{
    public class Timeline : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private int _maxTimePerSecond = 10;

        public const float TimeStep = 0.1f;

        public int GetIndex
        {
            get
            {
                return (int)(_slider.value / TimeStep);
            }
        }
        public int MaxIndex
        {
            get
            {
                return (int)(_maxTimePerSecond / TimeStep);
            }
        }

        private void Awake()
        {
            _slider.maxValue = _maxTimePerSecond;   
        }
    }
}

