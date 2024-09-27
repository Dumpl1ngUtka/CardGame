using Battleground;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Marker
{
    public class ZoneMarker : Marker
    {
        private SimpleZone _simpleZone;
        private LayerMask _layerMask;

        protected override LayerMask Mask => _layerMask;


        public void Init(LayerMask layerMask, SimpleZone obj)
        {
            _layerMask = layerMask;
            _simpleZone = obj;
        }

        protected override void Render(RaycastHit point)
        {
            transform.position = point.point;
        }
    }
}

