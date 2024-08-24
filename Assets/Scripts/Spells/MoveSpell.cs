using Battleground;
using System.Collections;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        private const int _stepCount = 20;
        public Vector3 _movePosition;
        private Vector3 _startPosition;

        public override void Start()
        {
            
        }

        public override void Update()
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
                    Debug.Log("Ќе достаточно времен на выполнение заклинани€");
            }
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }

        public override void Release(int stepIndex)
        {
            Piece.transform.position = Vector3.Lerp(_startPosition, _movePosition, (float)stepIndex / _stepCount);            
        }
    }
}