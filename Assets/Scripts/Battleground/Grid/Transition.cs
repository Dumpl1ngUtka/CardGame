using UnityEngine;

namespace Battleground
{
    public class Transition
    {
        public readonly BattlegroundCell From;
        public readonly BattlegroundCell To;
        public readonly TransitionType Type;

        public Transition(BattlegroundCell from, BattlegroundCell to, TransitionType type) 
        {
            From = from;
            To = to;
            Type = type;
        }
    }
}
