using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace AI
{
    public class SituationAnalyzer
    {
        private IAIWeightPoint _selfWeight;
        private List<IAIWeightPoint> _weightPoints = new List<IAIWeightPoint>();
        private List<IAIWeightPoint> _alliesPoints = new List<IAIWeightPoint>();
        private List<IAIWeightPoint> _groupedWeightPoints = new List<IAIWeightPoint>();
        private IAIWeightPoint _strongestEnemy;
        private IAIWeightPoint _weakestEnemy;
        private IAIWeightPoint _closestEnemy;
        private const float _checkSphereRadius = 20;

        public IAIWeightPoint SelfWeight => _selfWeight;
        public List<IAIWeightPoint> WeightPoints => _weightPoints;
        public List<IAIWeightPoint> AlliesPoints => _alliesPoints;
        public List<IAIWeightPoint> GroupedWeightPoints => _groupedWeightPoints;
        public IAIWeightPoint ClosestEnemy => _closestEnemy;

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
            _weightPoints.Clear();
            _alliesPoints.Clear();
            var minDistance = 0f;
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    if (weightPoint == _selfWeight)
                        continue;

                    if (weightPoint.TeamID == _selfWeight.TeamID)
                    {
                        
                        _alliesPoints.Add(weightPoint);
                    }
                    else
                    {
                        _weightPoints.Add(weightPoint);

                        var distance = Vector3.Distance(weightPoint.Position, _selfWeight.Position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            _closestEnemy = _selfWeight;
                        }
                    }
                }
            }
        }

        public List<IAIWeightPoint> GetAlliesPoints()
        {
            var allies = new List<IAIWeightPoint>();
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    if (weightPoint != _selfWeight && weightPoint.TeamID == _selfWeight.TeamID) 
                        allies.Add(weightPoint);
                }
            }
            return allies;
        }

        public List<IAIWeightPoint> GetWeightPoints()
        {
            var weightPoints = new List<IAIWeightPoint>();
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    if (weightPoint == _selfWeight)
                        continue;
                    weightPoints.Add(weightPoint);
                }
            }
            return weightPoints;
        }

        public List<IAIWeightPoint> GetWeightGrouped(List<IAIWeightPoint> points)
        {
            var groupIndexes = new List<int>(points.Count);
            var groupCount = 0;
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (points[j] == points[i])
                        continue;

                    if (Vector3.Distance(points[j].Position, points[i].Position) < 5f)
                    {
                        groupIndexes[i] = groupIndexes[j];
                        break;  
                    }
                }
                if (groupIndexes[i] == 0)
                    groupIndexes[i] = ++groupCount;
            }
            var groupedPoints = new List<AIWeightPoint>();
            for (int i = 0; i < points.Count; i++)
            {
                groupedPoints.Add(new AIWeightPoint(points[i]));
                for (int j = 0; j < groupIndexes.Count; j++)
                {
                    if (groupIndexes[j] == groupIndexes[i])
                    {
                        groupedPoints[i].Add(points[j]);
                    }
                }
            }
            return groupedPoints.Select(x => x as IAIWeightPoint).ToList();
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


        public void Add(IAIWeightPoint weightPoint)
        {
            _dangerWeight += weightPoint.DangerWeight;
            _chargedSkillsDamage += weightPoint.ChargedSkillsDamage;
            _damagePerMinute += weightPoint.DamagePerMinute;
            _missingHealth += weightPoint.MissingHealth;
            _currentHealth += weightPoint.CurrentHealth;
        }
    }
}

