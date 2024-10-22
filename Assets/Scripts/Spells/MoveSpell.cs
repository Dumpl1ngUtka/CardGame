using Battleground;
using UnityEngine;
using UnityEngine.AI;
using UI.Marker;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        [SerializeField] private float _distancePerSecond;

        [SerializeField] private RuntimeAnimatorController _controller;

        private NavMeshAgent _agent; 

        public Vector3[] Path => _agent.path.corners; 

        public override void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (_agent.enabled)
                    _agent.destination = hit.point;
            }
        }

        public override void StartRelease()
        {
            _agent = Piece.Agent;
            _agent.enabled = true;
        }

        public override void EndRelease()
        {
            _agent.enabled = false;
        }
    }
}