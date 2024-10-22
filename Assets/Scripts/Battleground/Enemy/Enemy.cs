using AI;
using UnityEngine;

namespace Battleground
{
    public class Enemy : MonoBehaviour, IAIWeightPoint
    {
        [SerializeField] private float _dangerWeight;
        [SerializeField] private float _damagePerMinute;
        [SerializeField] private float _chargedSkillsDamage;
        public Transform Transform => transform;

        public Vector3 Position => transform.position;

        public float TeamID => 1;

        public float DangerWeight => _dangerWeight;

        public float ChargedSkillsDamage => _chargedSkillsDamage;

        public float DamagePerMinute => _damagePerMinute;

        public float MissingHealth => 111;

        public float CurrentHealth => 46;
    }
}