using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class SituationAnalyzer
    {
        private IAIWeightPoint _selfWeight;

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


        public SituationAnalyzer(IAIWeightPoint piece)
        {
            _selfWeight = piece;
            UpdateMatrices();
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
}

