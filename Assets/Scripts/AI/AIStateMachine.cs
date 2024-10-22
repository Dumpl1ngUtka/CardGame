using System.Collections.Generic;
using Units;
using UnityEditor;
using UnityEngine;

namespace AI
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField] private float _dangerWeight;
        [SerializeField] private float _maxHealht;
        [SerializeField, Range(0,1)] private float _healthValue;
        [SerializeField] private float _teamID;
        [SerializeField] private List<Spell> _spells;
        private float _health;
        private float _currentHealth;

        private SituationAnalyzer _analyzer;

        public Vector3 Position => transform.position;

        public float TeamID => _teamID;

        public float DangerWeight => throw new System.NotImplementedException();

        public float ChargedSkillsDamage => throw new System.NotImplementedException();

        public float DamagePerMinute => throw new System.NotImplementedException();

        public float MissingHealth => _maxHealht - _currentHealth;

        public float CurrentHealth => _currentHealth;

        private void Awake()
        {
            //_analyzer = new SituationAnalyzer();
        }
    }
}
