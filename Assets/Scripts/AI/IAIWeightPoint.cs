using UnityEngine;

namespace AI
{
    public interface IAIWeightPoint
    {
        #region Base
        public Transform Transform { get; }
        public Vector3 Position { get; }
        public float TeamID { get; }
        #endregion

        #region Damage
        public float DangerWeight { get; }
        public float ChargedSkillsDamage { get; }
        public float DamagePerMinute { get; }
        #endregion

        #region HealAndHealth
        public float MissingHealth { get; }
        public float CurrentHealth { get; }

        #endregion
    }
}

