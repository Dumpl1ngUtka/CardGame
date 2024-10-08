using Battleground;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MeleeAttack")]

    public class MeleeAttackSpell : Spell
    {
        [Min(0)] public float Offset;
        [SerializeField] private MeleeAttackZone _meleeZonePrefab;

        private Vector3 _direction;
        private Vector3 _startPosition;
        private MeleeAttackZone _hitbox;

        private MeleeMarker _marker;

        public override void LeftMouseClick(RaycastHit hit)
        {
            _direction = hit.point;
            StartTime = Piece.Player.Timeline.GetTime;
            if (Piece.AddActivity(this))
            {
                _hitbox = Instantiate(_meleeZonePrefab);
                _hitbox.Init(Piece, this, _direction, ActionTime * 0.8f + StartTime);
                IsSpellFinished = true;
            }
            else
                Debug.Log("Ќедостаточно времени на выполнение заклинани€");
        }
        public override void Release(float time = 0)
        {
            Piece.Animator.Play("SwordAttack");
            Piece.Animator.SetFloat("Progress", time / ActionTime);
        }

        public override void RightMouseClick(RaycastHit hit)
        {
        }

        public override void RemoveFromTimeline()
        {
            if (_hitbox.gameObject != null)
                Destroy(_hitbox.gameObject);
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
            _startPosition = Piece.transform.position;
            _marker = Instantiate(MarkerPrefab) as MeleeMarker;
            _marker.Init(Piece, Mask, _meleeZonePrefab, _startPosition);
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);

        }
    }
}