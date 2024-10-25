using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class RunAway : PieceState
    {
        public RunAway(PieceStateMachine pieceStateMachine) : base(pieceStateMachine)
        {
        }

        protected override PieceState CheckTransitionConditions()
        {
            throw new System.NotImplementedException();
        }
    }
}