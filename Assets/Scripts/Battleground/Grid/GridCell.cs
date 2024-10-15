using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.Grid
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private Vector4 _gridPosition;
        [SerializeField] private bool _setNeighbors = false;
        private MaterialPropertyBlock _propertyBlock;
        private MeshRenderer _meshRenderer;

        public List<GridCell> Neighbors;
        public Vector4 GridPosition => _gridPosition;
        public Vector3 WorldPosition => transform.position;
        public Vector3 SpawnPosition => transform.position + Vector3.up;

        public Piece Piece { get; private set; }

        public void SetPiece(Piece piece)
        {
            Piece = piece;
        }

        public void RemovePiece()
        {
            Piece = null;
        }

        public void SetColor(Color color)
        {
            if (_propertyBlock == null)
                _propertyBlock = new MaterialPropertyBlock();

            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();

            _propertyBlock.SetColor("_Color", color);
            _meshRenderer.SetPropertyBlock(_propertyBlock);
        }

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _meshRenderer = GetComponent<MeshRenderer>();
            
        }

        private void OnValidate()
        {
            if (_setNeighbors)
            {
                _setNeighbors = false;
                SetNeighbors();
            }
        }

        private void SetNeighbors()
        {
            Neighbors = new List<GridCell>();
            foreach (var collider in Physics.OverlapSphere(transform.position, 2f))
            {
                var cell = collider.GetComponent<GridCell>();
                if (cell.GridPosition == GridPosition)
                    continue;
                Neighbors.Add(cell);
            }
        }

        public static int GetDistance(GridCell from, GridCell to)
        {
            var delta = from.GridPosition - to.GridPosition;
            return (Math.Abs((int)delta.x) + Math.Abs((int)delta.y) + Math.Abs((int)delta.z) + Math.Abs((int)delta.w)) / 2;
        }
    }
}