using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public class ZoneMarker : Marker
    {
        private SimpleZone _simpleZone;

        public override void Init(SpellObject obj, Vector3 startPos)
        {
            _simpleZone = obj as SimpleZone;
        }

        protected override void Render(RaycastHit point)
        {
            transform.position = point.point;
        }
    }
}

