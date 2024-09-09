using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/TestSpell")]
    public class TestSpell : Spell
    {
        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                StartTime = Piece.Player.Timeline.GetTime;
                if (Piece.AddActivity(this))
                    IsSpellFinished = true;
                else
                    Debug.Log("Ќедостаточно времен на выполнение заклинани€");
            }
            IsSpellFinished = true;
        }

        public override void Release(float stepIndex)
        {
            Debug.Log(stepIndex);
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            //throw new System.NotImplementedException();
        }

        public override void RemoveFromTimeline()
        {
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }

        public override void StartRelease()
        {
            //throw new System.NotImplementedException();
        }

        public override void EndRelease()
        {
            //throw new System.NotImplementedException();
        }
    }

}