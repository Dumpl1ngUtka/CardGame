using Battleground.Grid;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class MeleeAttackZone : SpellObject
    {
        private List<IDamageable> _collidedObjects;
        private GridCell _position;

        public void Init(Piece piece, MeleeAttackSpell spell, GridCell position,float startTime)
        {
            base.Init(piece, startTime);
            _position = position;
            _collidedObjects = new List<IDamageable>();
        }

        public override void MoveByTimeline(float time, bool isSimulation = false)
        {
            base.MoveByTimeline(time, isSimulation);

            if (isSimulation)
            {
                if (_position.Piece is IDamageable damagebleObj)
                    damagebleObj.ApplyDamage(new Damage(_damage, damagebleObj, _piece));
                Destroy(gameObject);
            }
        }

        public Vector3 GetHitBoxRotation(Vector3 startPos, Vector3 pointPosition)
        {
            var angle = Vector3.Angle((Vector3.forward), (pointPosition - startPos).normalized);
            var sign = pointPosition.x > startPos.x ? 1 : -1;
            return new Vector3(0, sign * angle, 0);
        }
    }
}

