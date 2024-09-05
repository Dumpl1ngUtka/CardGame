using Battleground;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        public Vector3 _movePosition;
        private Vector3 _startPosition;

        public override void RemoveFromTimeline()
        {

        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                var inst = Instantiate(this);
                inst.Init(Piece);
                inst._movePosition = hit.point;
                inst._startPosition = Piece.transform.position;
                inst.StartIndex = Piece.Player.Timeline.GetIndex;
                if (Piece.AddActivity(inst))
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
            Piece.transform.position = Vector3.Lerp(_startPosition, _movePosition, time / StepCount);            
        }

        public override void Update()
        {
        }

        public override void Start()
        {
        }
    }
}