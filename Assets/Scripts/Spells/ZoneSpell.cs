using Battleground;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/SimpleZone")]

    public class ZoneSpell : Spell
    {
        [SerializeField] private SimpleZone _zonePrefab;

        private Vector3 _targetPosition;
        private SimpleZone _zone;
        
        public override void Update()
        {
        }

        public override void StartRelease()
        {
        }

        public override void EndRelease()
        {
        }
    }
}