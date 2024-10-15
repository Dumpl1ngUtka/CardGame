using Battleground;
using Battleground.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Marker
{
    public class MeleeMarker : Marker
    {
        [SerializeField] private GameObject _targetObject;

        private MeleeAttackZone _hitBox;
        private GridCell _markerPos;
        private List<GridCell> _availableCells;
        private LayerMask _layerMask;

        protected override LayerMask Mask => _layerMask;

        public void Init(Piece piece, LayerMask layerMask, MeleeAttackZone obj, List<GridCell> availableCells)
        {
            _layerMask = layerMask;
            _hitBox = obj;
            _availableCells = availableCells;
        }

        public GridCell MarkerPos() => _markerPos;

        protected override void Render(RaycastHit point)
        {
            var cell = point.collider.GetComponent<GridCell>();
            if (cell != _markerPos && _availableCells.Contains(cell))
            {
                transform.position = cell.SpawnPosition;
                _markerPos = cell;
            }
        }
    }
}

