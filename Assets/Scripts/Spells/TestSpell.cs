using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/TestSpell")]
    public class TestSpell : Spell
    {
        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                var timelineIndex = Piece.Player.Timeline.GetIndex;
                Piece.Activities.AddAction(new Activity(Releasing(), 90, 0));
            }
            IsSpellFinished = true;
        }

        public override IEnumerator Releasing()
        {
            var count = 0;
            while (count < 90)
            {
                Debug.Log(count++);
                yield return null;
            }
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {

        }
    }

}