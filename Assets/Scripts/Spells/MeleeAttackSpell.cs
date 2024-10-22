using Battleground;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MeleeAttack")]

    public class MeleeAttackSpell : Spell
    {
        [SerializeField] private MeleeAttackZone _meleeZonePrefab;

        private Vector3 _direction;
        private Vector3 _startPosition;
        private MeleeAttackZone _hitbox;
        
        public override void Update()
        {
        }

        public override void StartRelease()
        {
            _startPosition = Piece.transform.position;
        }

        public override void EndRelease()
        {
        }
    }
}