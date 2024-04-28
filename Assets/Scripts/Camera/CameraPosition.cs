using UnityEngine;

namespace Battleground
{
    public class CameraPosition
    {
        public readonly float Height;
        public readonly Vector3 Rotation;
        public CameraPosition(float height, Vector3 rotation)
        {
            Height = height;
            Rotation = rotation;
        }
    }
}