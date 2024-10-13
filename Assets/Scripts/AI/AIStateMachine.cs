using UnityEditor;
using UnityEngine;

namespace AI
{
    public class AIStateMachine : MonoBehaviour, IAIWeightPoint
    {
        [SerializeField] private float _dangerWeight;
        [SerializeField] private float _healingNeededWeight;
        [SerializeField] private float _teamID;
     

        private SituationAnalyzer _analyzer;
        public Vector3 Position => transform.position;
        public float DangerWeight => _dangerWeight;
        public float HealingNeededWeight => _healingNeededWeight;
        public float TeamID => _teamID;

        private void Awake()
        {
            _analyzer = new SituationAnalyzer();
        }
    }
}
