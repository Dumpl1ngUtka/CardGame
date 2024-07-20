using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        public override void Start()
        {
            
        }

        public override void Update()
        {

        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            Piece.Move(hit.point);
            IsSpellFinished = true;
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }
    }
}