using Battleground;
using System.Collections;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        private const int stepCount = 20;
        private Vector3 _movePosition;

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
                _movePosition = hit.point;
                var timelineIndex = Piece.Player.Timeline.GetIndex;
                Piece.Activities.AddAction(new Activity(Releasing(), stepCount, timelineIndex));
            }
            IsSpellFinished = true;
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }

        public override IEnumerator Releasing()
        {
            var currentStep = 0;
            while (currentStep < stepCount)
            {
                this.Piece.transform.position = Vector3.Lerp(this.Piece.transform.position, _movePosition, (float)currentStep++ / stepCount);
                yield return null;
            }
        }
    }
}