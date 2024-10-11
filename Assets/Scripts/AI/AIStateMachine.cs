using UnityEngine;

namespace AI
{
    public class AIStateMachine : MonoBehaviour, IAIWeightPoint
    {
        [SerializeField] private float _dangerWeight;
        public Vector3 Position => transform.position;
        public float DangerWeight => _dangerWeight;
    }
}
