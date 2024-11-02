using AI;
using UnityEngine;

namespace Battleground
{
    public interface IDamageAbility 
    {
        public float Damage { get; }
        public float DPM { get; }
    }

    public interface IMoveAbility
    {
        public float DistancePerSecond { get; }
    }

    public interface IHealAbility
    {
        public float OneShotHeal { get; }
        public float HealPerMinute { get; }
    }

    public interface IBuffAbility
    {

    }
}

