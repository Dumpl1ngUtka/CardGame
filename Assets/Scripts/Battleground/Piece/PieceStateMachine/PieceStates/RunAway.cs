using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class RunAway : PieceState
    {
        protected override float MinStateTime => 3;
        
        public RunAway(PieceStateMachine pieceStateMachine) : base(pieceStateMachine)
        {
        }

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            return -0.1f;
        }
    }
}