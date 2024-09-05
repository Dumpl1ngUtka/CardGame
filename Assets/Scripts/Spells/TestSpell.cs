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
                var inst = Instantiate(this);
                inst.Init(Piece);
                inst.StartIndex = Piece.Player.Timeline.GetIndex;
                if (Piece.AddActivity(inst))
                    IsSpellFinished = true;
                else
                    Debug.Log("Ќе достаточно времен на выполнение заклинани€");
            }
            IsSpellFinished = true;
        }

        public override void Release(float stepIndex)
        {
            Debug.Log(stepIndex);
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveFromTimeline()
        {
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }
    }

}