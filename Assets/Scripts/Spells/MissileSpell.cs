using Battleground;
using UI.Marker;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/SimpleMissileOrZone")]

    public class MissileSpell : Spell
    {
        [SerializeField] private SpellObject _missilePrefab;

        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private SpellObject _missile;

        private Marker _marker;

        public override void LeftMouseClick(RaycastHit hit)
        {
            _targetPosition = hit.point;
            StartTime = Piece.Player.Timeline.GetTime;
            if (Piece.AddActivity(this))
            {
                _missile = Instantiate(_missilePrefab);
                _missile.Init(Piece, _startPosition, _targetPosition, ActionTime * 0.7f + StartTime);
                IsSpellFinished = true;
            }
            else
                Debug.Log("������������ ������� �� ���������� ����������");
        }
        public override void Release(float time = 0)
        {
            Piece.Animator.Play("CastSpell");
            Piece.Animator.SetFloat("Progress", time/ActionTime);
        }

        public override void RightMouseClick(RaycastHit hit)
        {
        }

        public override void RemoveFromTimeline()
        {
            if (_missile.gameObject != null)
                Destroy(_missile.gameObject); 
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
            _startPosition = Piece.transform.position;
            _marker = Instantiate(MarkerPrefab);
            _marker.Init(_missilePrefab, _startPosition);
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);
        }
    }
}