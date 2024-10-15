using Battleground;
using Battleground.Grid;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Marker
{
    public class ZoneMarker : Marker
    {
        [SerializeField] private List<GameObject> _radiuses;
        private SimpleZone _simpleZone;
        private LayerMask _layerMask;
        private Piece _piece;
        private List<GridCell> _availableCells;
        private GridCell _markerPos;

        protected override LayerMask Mask => _layerMask;

        public void Init(Piece piece, LayerMask layerMask, SimpleZone obj, List<GridCell> availableCells)
        {
            _piece = piece;
            _availableCells = availableCells;
            _layerMask = layerMask;
            _simpleZone = obj;
            SetRadius(_simpleZone.Radius);
        }

        public GridCell MarkerPos() => _markerPos;

        private void SetRadius(int radiusValue)
        {
            radiusValue = Math.Clamp(radiusValue, 1, _radiuses.Count);
            int index = 1;
            foreach (GameObject radius in _radiuses)
            {
                if (index++ <= radiusValue)
                    radius.SetActive(true);
                else
                    radius.SetActive(false);
            }
        }

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

