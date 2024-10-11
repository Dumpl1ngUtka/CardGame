using UnityEngine;

namespace AI
{
    public class SituationAnalyzer : MonoBehaviour
    {
        #region Matrix Parameters
        private const float _checkSphereRadius = 10;
        private const int _levelCount = 3;
        private const int _sectorCount = 8;
        #endregion
        #region Matrices
        private WorldSituationMatrix _enemyMatrix;
        private WorldSituationMatrix _alliesMatrix;
        private WorldSituationMatrix _dangerMatrix;
        private WorldSituationMatrix _rewardMatrix;
        #endregion

        private void Awake()
        {
            var a = GetDangerMatrix();
            a.GetInfo();
        }

        public WorldSituationMatrix GetDangerMatrix()
        {
            var matrix = new WorldSituationMatrix(_checkSphereRadius, _levelCount, _sectorCount);
            foreach (var collider in Physics.OverlapSphere(transform.position, _checkSphereRadius))
            {
                if (collider.TryGetComponent<IAIWeightPoint>(out var weightPoint))
                {
                    var distance = (int)Vector3.Distance(transform.position, weightPoint.Position);
                    var angle = GetAngle360To(weightPoint.Position);
                    matrix[distance, angle] += weightPoint.DangerWeight;
                }
            }
            return matrix;
        }

        private int GetAngle360To(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0;
            var angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
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

        public float this[int distance, int degree]
        {
            get => _cells[(int)(distance / _oneLevelDistance) * SectorCount + (degree / _oneSectorDegree)].Value;
            set => _cells[(int)(distance / _oneLevelDistance) * SectorCount + (degree / _oneSectorDegree)].Value = value;
        }

        public WorldSituationMatrix(float maxDistance, int levelCount, int sectorCount)
        {
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

        public void GetInfo()
        {
            foreach (var cell in _cells)
                Debug.Log(cell.Distance + " " + cell.SectorDegrees + " " + cell.Value);
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

