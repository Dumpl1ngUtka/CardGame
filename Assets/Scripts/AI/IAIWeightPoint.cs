using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public interface IAIWeightPoint
    {
        public bool IsValid { get; }

        #region Base
        public Transform Transform { get; }
        public Vector3 Position { get; }
        public int TeamID { get; }
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

