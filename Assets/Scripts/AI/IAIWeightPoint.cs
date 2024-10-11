using UnityEngine;

namespace AI
{
    public interface IAIWeightPoint
    {
        public Vector3 Position { get; }
        public float DangerWeight { get; }
    }
}

