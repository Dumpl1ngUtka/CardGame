using Battleground;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/SimpleMissile")]

    public class MissileSpell : Spell
    {
        [SerializeField] private SimpleMissile _missilePrefab;

        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private SimpleMissile _missile;

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