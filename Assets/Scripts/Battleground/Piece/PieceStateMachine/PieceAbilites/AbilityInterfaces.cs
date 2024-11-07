using AI;
using UnityEngine;

namespace Battleground
{
    public interface IDamageAbility 
    {
        public PieceAbility Ability { get; }
        public float Damage { get; }
        public float DPM { get; }
        public Transform Target { get; }
    }

    public interface IMoveAbility
    {
        public PieceAbility Ability { get; }
        public float DistancePerSecond { get; }

    }

    public interface IHealAbility
    {
        public PieceAbility Ability { get; }
        public float OneShotHeal { get; }
        public float HealPerMinute { get; }
    }

    public interface IBuffAbility
    {
        public PieceAbility Ability { get; }
    }
}

