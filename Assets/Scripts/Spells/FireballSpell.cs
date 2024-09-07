using Battleground;
using System.Runtime.InteropServices;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/Fireball")]

    public class FireballSpell : Spell
    {
        [SerializeField] private Fireball _fireballPrefab;

        public Vector3 _targetPosition;
        public Vector3 _startPosition;
        public Fireball _fireball;

        private Marker _marker;

        public override void LeftMouseClick(RaycastHit hit)
        {
            _targetPosition = hit.point;
            StartIndex = Piece.Player.Timeline.GetIndex;
            if (Piece.AddActivity(this))
            {
                _fireball = Instantiate(_fireballPrefab);
                _fireball.Init(Piece.Player, _startPosition, _targetPosition, EndIndex);
                IsSpellFinished = true;
            }
            else
                Debug.Log("Ќедостаточно времени на выполнение заклинани€");
        }
        public override void Release(float time = 0)
        {
            //play anim
        }

        public override void RightMouseClick(RaycastHit hit)
        {
        }

        public override void RemoveFromTimeline()
        {
            Destroy(_fireball.gameObject); 
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
            _startPosition = Piece.transform.position;
            _marker = Instantiate(MarkerPrefab);
            _marker.Init(_fireballPrefab, _startPosition);
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);
        }
    }
}