using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class RunAway : PieceState
    {
        protected override float MinStateTime => 3;
        protected override float MaxStateTime => float.PositiveInfinity;

        protected override List<PieceAbility> AvailableAbilityList => throw new System.NotImplementedException();

        public override Transform Target => throw new System.NotImplementedException();

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            return -0.1f;
        }
    }
}