using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace AI
{
    public class SituationAnalyzer
    {
        private IAIWeightPoint _selfWeight;
        private IAIWeightPoint _selfGroupWeight;
        private List<IAIWeightPoint> _enemyPoints = new List<IAIWeightPoint>();
        private List<IAIWeightPoint> _alliesPoints = new List<IAIWeightPoint>();
        private List<IAIWeightPoint> _groupedEnemyPoints = new List<IAIWeightPoint>();
        private IAIWeightPoint _strongestEnemy;
        private IAIWeightPoint _weakestEnemy;
        private IAIWeightPoint _closestEnemy;
        private const float _checkSphereRadius = 20;
        private const float _groupMaxInfluenceDistance = 5f;
        private const float _groupNoInfluenceDistance = 10f;

        public IAIWeightPoint SelfWeight => _selfWeight;
        public IAIWeightPoint SelfGroupWeight => _selfGroupWeight;
        public List<IAIWeightPoint> EnemyPoints => _enemyPoints;
        public List<IAIWeightPoint> AlliesPoints => _alliesPoints;
        public List<IAIWeightPoint> GroupedEnemyPoints => _groupedEnemyPoints;
        public IAIWeightPoint ClosestEnemy => _closestEnemy;
        public IAIWeightPoint StrongestEnemy => _strongestEnemy;
        public IAIWeightPoint WeakestEnemy => _weakestEnemy;

        public SituationAnalyzer(IAIWeightPoint piece)
        {
            _selfWeight = piece;
            UpdateLists();
        }

        public void Update()
        {
            UpdateLists();
        }

        private void UpdateLists()
        {
            _enemyPoints.Clear();
            _alliesPoints.Clear();
            _groupedEnemyPoints.Clear();
            _closestEnemy = null;
            _strongestEnemy = null;
            _weakestEnemy = null;

            var minDistance = _checkSphereRadius;
            var maxDanger = 0f;
            var minDanger = float.MaxValue;
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    if (!weightPoint.IsValid || weightPoint == _selfWeight)
                        continue;

                    if (weightPoint.TeamID == _selfWeight.TeamID)
                    {
                        _alliesPoints.Add(weightPoint);
                    }
                    else
                    {
                        _enemyPoints.Add(weightPoint);

                        var distance = Vector3.Distance(weightPoint.Position, _selfWeight.Position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            _closestEnemy = weightPoint;
                        }

                        if (weightPoint.DangerWeight > maxDanger)
                        {
                            maxDanger = weightPoint.DangerWeight;
                            _strongestEnemy = weightPoint;
                        }

                        if (weightPoint.DangerWeight < minDanger)
                        {
                            minDanger = weightPoint.DangerWeight;
                            _weakestEnemy = weightPoint;
                        }
                    }
                }
            }
            _groupedEnemyPoints = GetWeightGrouped(_enemyPoints);
            _selfGroupWeight = GetInfluenceOfGroupOnPoint(_selfWeight, _alliesPoints);
            //Debug.Log("SelfDPM " + _selfWeight.DamagePerMinute + " GroupDPM " + _selfGroupWeight.DamagePerMinute);
        }

        public List<IAIWeightPoint> GetAlliesPoints()
        {
            var allies = new List<IAIWeightPoint>();
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    if (weightPoint.IsValid && weightPoint != _selfWeight && weightPoint.TeamID == _selfWeight.TeamID) 
                        allies.Add(weightPoint);
                }
            }
            return allies;
        }

        public List<IAIWeightPoint> GetWeightGrouped(List<IAIWeightPoint> points)
        {
            var groupedPoints = new List<IAIWeightPoint>();

            foreach (var point in points)
                if (point.IsValid)
                    groupedPoints.Add(GetInfluenceOfGroupOnPoint(point, points));

            return groupedPoints;
        }

        public IAIWeightPoint GetInfluenceOfGroupOnPoint(IAIWeightPoint point, List<IAIWeightPoint> group)
        {
            var groupedPoint = new AIWeightPoint(point);
            foreach (var otherPoint in group)
            {
                if (!otherPoint.IsValid || otherPoint == point)
                    continue;

                var distance = Vector3.Distance(point.Position, otherPoint.Position);
                var influence = (_groupNoInfluenceDistance - distance) / (_groupNoInfluenceDistance - _groupMaxInfluenceDistance);
                influence = Mathf.Clamp01(influence);
                groupedPoint.Add(otherPoint, influence);
            }
            return groupedPoint;
        }
    }

    public struct AIWeightPoint : IAIWeightPoint
    {
        private Transform _transform;

        private Vector3 _position;

        private int _teamID;

        private float _dangerWeight;

        private float _chargedSkillsDamage;

        private float _damagePerMinute;

        private float _missingHealth;

        private float _currentHealth;

        public bool IsValid => true;

        public Transform Transform => _transform;

        public Vector3 Position => _position;

        public int TeamID => _teamID;

        public float DangerWeight => _dangerWeight;

        public float ChargedSkillsDamage => _chargedSkillsDamage;

        public float DamagePerMinute => _damagePerMinute;

        public float MissingHealth => _missingHealth;

        public float CurrentHealth => _currentHealth;

        public AIWeightPoint(IAIWeightPoint baseWeightPoint)
        {
            _transform = baseWeightPoint.Transform;
            _position = baseWeightPoint.Position;
            _teamID = baseWeightPoint.TeamID;

            _dangerWeight = baseWeightPoint.DangerWeight;
            _chargedSkillsDamage = baseWeightPoint.ChargedSkillsDamage;
            _damagePerMinute = baseWeightPoint.DamagePerMinute;
            _missingHealth = baseWeightPoint.MissingHealth;
            _currentHealth = baseWeightPoint.CurrentHealth;
        }


        public void Add(IAIWeightPoint weightPoint, float influence = 1f)
        {
            _dangerWeight += weightPoint.DangerWeight * influence;
            _chargedSkillsDamage += weightPoint.ChargedSkillsDamage * influence;
            _damagePerMinute += weightPoint.DamagePerMinute * influence;
            _missingHealth += weightPoint.MissingHealth * influence;
            _currentHealth += weightPoint.CurrentHealth * influence;
        }
    }
}

