using Battleground;
using Units;
using UnityEngine;

namespace UI.Marker
{
    public class MoveMarker : Marker
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private MoveSpell _moveSpell;

        protected override LayerMask Mask { get => _moveSpell.Mask; }

        public void Init(MoveSpell moveSpell)
        {
            _moveSpell = moveSpell;
        }

        protected override void Render(RaycastHit point)
        {
            var corners = _moveSpell.Path;
            _lineRenderer.positionCount = corners.Length;
            for (int i = 0; i < corners.Length; i++)
            {
                _lineRenderer.SetPosition(i, corners[i] + Vector3.up);
            }
        }
    }
}
