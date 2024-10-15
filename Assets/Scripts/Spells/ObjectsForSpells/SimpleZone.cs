using Battleground.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class SimpleZone : SpellObject
    {
        [SerializeField] private float _damagePeriod;
        [SerializeField] private int _maxDistance;
        [SerializeField] private int _radius;
        private int damageCount;
        private List<GridCell> _cells;

        public int MaxDistance
        {
            get 
            {
                var value = _maxDistance;
                if (_piece != null)
                    value += _piece.Attributes.DistanceAttackDistancePercent;
                return value;
            }
        }
        public int Radius
        {
            get
            {
                var value = _radius;
                if (_piece != null)
                    value += _piece.Attributes.DistanceAttackRangePercent;
                return value;
            }
        }

        public void Init(Piece piece, GridCell centerPosition, float startTime)
        {
            base.Init(piece, startTime);
            _cells = new List<GridCell>();
            foreach (var cell in piece.Player.Map.GridCells)
            {
                if (GridCell.GetDistance(centerPosition, cell) <= _radius)
                    _cells.Add(cell);
            } 
        }

        public override void MoveByTimeline(float time, bool isSimulation = false)
        {
            base.MoveByTimeline(time, isSimulation);

            if (isSimulation)
            {
                if (time > _damagePeriod * damageCount)
                {
                    foreach (var obj in GetDamagableObjcects())
                    {
                        obj.ApplyDamage(new Damage(_damage, obj));
                    }
                    damageCount++;
                }
            }
        }

        private List<IDamageable> GetDamagableObjcects()
        {
            var objects = new List<IDamageable>();
            foreach (var cell in _cells)
                if (cell.Piece != null && cell.Piece is IDamageable damagebleObj)
                    objects.Add(damagebleObj);
            return objects;
        }

        public override void NextMove()
        {
        }
    }
}

