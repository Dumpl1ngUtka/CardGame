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

        private ZoneMarker _marker;

        public override void LeftMouseClick(RaycastHit hit)
        {
            _targetPosition = hit.point;
            StartTime = Piece.Player.Timeline.GetTime;
            if (Piece.AddActivity(this))
            {
                _zone = Instantiate(_zonePrefab);
                _zone.Init(Piece, _targetPosition, ActionTime * 0.7f + StartTime);
                IsSpellFinished = true;
            }
            else
                Debug.Log("Ќедостаточно времени на выполнение заклинани€");
        }
        public override void Release(float time = 0)
        {
            Piece.Animator.Play("MagicSpell");
            Piece.Animator.SetFloat("Progress", time/ActionTime);
        }

        public override void RightMouseClick(RaycastHit hit)
        {
        }

        public override void RemoveFromTimeline()
        {
            if (_zone.gameObject != null)
                Destroy(_zone.gameObject); 
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
            _zonePrefab.Init(Piece, StartTime);
            _marker = Instantiate(MarkerPrefab) as ZoneMarker;
            _marker.Init(Piece, Mask, _zonePrefab);
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);
        }
    }
}