using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class SimpleZone : SpellObject
    {
        public override void Init(Piece piece, Vector3 startPos, Vector3 endPos, float startTime)
        {
            base.Init(piece, startPos, endPos, startTime);
            transform.position = endPos;
        }

        public override void NextMove()
        {
        }
    }
}

