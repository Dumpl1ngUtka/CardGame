using System;
using UnityEngine;

namespace Battleground
{
    [Serializable]
    public struct AdditionalPieceAttributes
    {
        [Header("Health Attributes")]
        public float Health;
        public float HealthPercent;
        [Space, Header("Weapon Attributes")]
        public float AccuracyPercent;
        public float MeleeAttackRagngeAdditionPercent;
        [Space, Header("Protection Attributes")]
        public float DodgeChancePercent;
        public float BlockChancePercent;
        [Space, Header("Other")]
        public float MaxWeight;

        public static AdditionalPieceAttributes operator +(AdditionalPieceAttributes a, AdditionalPieceAttributes b)
        {
            var newAttributes = new AdditionalPieceAttributes
            {
                Health = a.Health + b.Health,
                HealthPercent = a.HealthPercent + b.HealthPercent,
                AccuracyPercent = a.AccuracyPercent + b.AccuracyPercent,
                DodgeChancePercent = a.DodgeChancePercent + b.DodgeChancePercent,
                BlockChancePercent = a.BlockChancePercent + b.BlockChancePercent,
                MeleeAttackRagngeAdditionPercent = a.MeleeAttackRagngeAdditionPercent + b.MeleeAttackRagngeAdditionPercent,
                MaxWeight = a.MaxWeight + b.MaxWeight
            };
            return newAttributes;
        }
    }
}