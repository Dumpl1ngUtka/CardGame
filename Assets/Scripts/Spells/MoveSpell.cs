using Battleground;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        [SerializeField] private float _distancePerSecond;
        private Vector3 _movePosition;
        private Vector3 _startPosition;

        public override void RemoveFromTimeline()
        {

        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                _movePosition = hit.point;
                _startPosition = Piece.transform.position;
                StartTime = Piece.Player.Timeline.GetTime;
                ActionTime = (_startPosition - _movePosition).magnitude / _distancePerSecond;
                if (Piece.AddActivity(this))
                    IsSpellFinished = true;
                else
                    Debug.Log("Ќедостаточно времени на выполнение заклинани€");
            }
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }

        public override void Release(float time)
        {
            Piece.transform.position = Vector3.Lerp(_startPosition, _movePosition, time / ActionTime);            
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
        }

        public override void EndRelease()
        {
            //throw new System.NotImplementedException();
        }
    }
}