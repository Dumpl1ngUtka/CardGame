using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class SituationAnalyzer
    {
        private IAIWeightPoint _selfWeight;
        private List<IAIWeightPoint> _weightPoints;
        private List<IAIWeightPoint> _groupedWeightPoints;

        #region Matrix Parameters
        private const float _checkSphereRadius = 10;
        private const int _levelCount = 3;
        private const int _sectorCount = 8;
        #endregion

        #region Matrices
        private WorldSituationMatrix _dangerMatrix = new(_checkSphereRadius, _levelCount, _sectorCount);
        private WorldSituationMatrix _DPSMatrix = new(_checkSphereRadius, _levelCount, _sectorCount);
        private WorldSituationMatrix _healthMatrix = new(_checkSphereRadius, _levelCount, _sectorCount);
        private WorldSituationMatrix _enemyMatrix = new(_checkSphereRadius, _levelCount, _sectorCount);
        private WorldSituationMatrix _alliesMatrix = new(_checkSphereRadius, _levelCount, _sectorCount);
        private WorldSituationMatrix _rewardMatrix = new(_checkSphereRadius, _levelCount, _sectorCount);

        public WorldSituationMatrix DangerMatrix => _dangerMatrix;
        public WorldSituationMatrix DPSMatrix => _DPSMatrix;
        public WorldSituationMatrix HealthMatrix => _healthMatrix;
        public WorldSituationMatrix EnemyMatrix => _enemyMatrix;
        public WorldSituationMatrix AlliesMatrix => _alliesMatrix;
        public WorldSituationMatrix RewardMatrix => _rewardMatrix;
        #endregion

        public List<IAIWeightPoint> WeightPoints => _weightPoints;
        public List<IAIWeightPoint> GroupedWeightPoints => _groupedWeightPoints;

        public SituationAnalyzer(IAIWeightPoint piece)
        {
            _selfWeight = piece;
        }

        public void Update()
        {
            _weightPoints = GetWeightPoints();
            //_groupedWeightPoints = GetWeightGrouped(WeightPoints);
        }

        public List<IAIWeightPoint> GetAlliesPoints()
        {
            var allies = new List<IAIWeightPoint>();
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius - 1))
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
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius - 1))
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

        public void UpdateMatrices()
        {
            ClearMatrices();
            foreach (var collider in Physics.OverlapSphere(_selfWeight.Position, _checkSphereRadius - 1))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    if (weightPoint == _selfWeight)
                        continue;
                    var distance = (int)Vector3.Distance(_selfWeight.Position, weightPoint.Position);
                    var angle = GetAngle360To(weightPoint.Position);
                    var isTeammate = weightPoint.TeamID == _selfWeight.TeamID;
                    _dangerMatrix[distance, angle] += isTeammate ? -weightPoint.DangerWeight : weightPoint.DangerWeight;
                    _DPSMatrix[distance, angle] += isTeammate ? -weightPoint.DamagePerMinute: weightPoint.DamagePerMinute;
                    _alliesMatrix[distance, angle] += isTeammate ? 1 : 0;
                    _enemyMatrix[distance, angle] += isTeammate ? 0 : 1;
                }
            }
        }

        private void ClearMatrices()
        {
            _dangerMatrix.Clear();
            _enemyMatrix.Clear();
            _alliesMatrix.Clear();
            _rewardMatrix.Clear();
        }

        private int GetAngle360To(Vector3 target)
        {
            var direction = target - _selfWeight.Position;
            direction.y = 0;
            var angle = Vector3.SignedAngle(_selfWeight.Transform.forward, direction, Vector3.up);
            if (angle < 0)
                angle += 360;
            return (int)angle;
        }
    }

    public struct WorldSituationMatrix
    {
        public readonly int LevelCount;
        public readonly int SectorCount;
        private readonly int _oneSectorDegree;
        private readonly float _oneLevelDistance;
        private WorldSituationMatrixCell[] _cells;

        public bool IsEmpty { get; private set; }

        public float this[int distance, int degree]
        {
            get => _cells[Mathf.FloorToInt(distance / _oneLevelDistance) * SectorCount + (degree / _oneSectorDegree)].Value;
            set
            {
                if (value != 0)
                    IsEmpty = false;
                _cells[Mathf.FloorToInt(distance / _oneLevelDistance) * SectorCount + (degree / _oneSectorDegree)].Value = value;
            }
        }

        public WorldSituationMatrix(float maxDistance, int levelCount, int sectorCount)
        {
            IsEmpty = true;
            LevelCount = levelCount;
            SectorCount = sectorCount;
            _oneSectorDegree = 360 / SectorCount;
            _oneLevelDistance = maxDistance / levelCount;
            _cells = new WorldSituationMatrixCell[levelCount * sectorCount];
            for (int i = 0; i < levelCount; i++)
            {
                for (int j = 0; j < sectorCount; j++)
                {
                    _cells[i * sectorCount + j] = new WorldSituationMatrixCell((int)(i * _oneLevelDistance), j * _oneSectorDegree);
                }
            }
        }

        public Dictionary<float,float> GetSectorAmounts()
        {
            var amounts = new Dictionary<float, float>();
            for (int j = 0; j < SectorCount; j++)
            {
                var degree = j * _oneSectorDegree;
                var sum = 0f;
                for (int i = 0; i < LevelCount; i++)
                {
                    sum += _cells[i * SectorCount + j].Value;
                }
                amounts.Add(degree, sum);
            }
            return amounts;
        }
        
        public void Clear()
        {
            IsEmpty = true;
            for (int i = 0; i < LevelCount; i++)
            {
                for (int j = 0; j < SectorCount; j++)
                {
                    _cells[i * SectorCount + j].Value = 0;
                }
            }
        }

        public void GetInfo()
        {
            foreach (var cell in _cells)
                Debug.Log(cell.Distance + " " + cell.SectorDegrees + " " + cell.Value);

            Debug.Log("IsEmpty: " + IsEmpty);
        }
    }

    public struct WorldSituationMatrixCell
    {
        public readonly int Distance;
        public readonly int SectorDegrees;
        public float Value;

        public WorldSituationMatrixCell(int distance, int setorDegrees)
        {
            Distance = distance;
            SectorDegrees = setorDegrees;
            Value = 0;
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

