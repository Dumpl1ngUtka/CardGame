using UnityEngine;

namespace Battleground
{
    public class SimpleMissile : SpellObject, ICameraPivot
    {
        [SerializeField] private float _distanceBySecond;
        [SerializeField] private float _arcSize;
        [SerializeField] private AnimationCurve _positionCurve;

        public override void Init(Piece piece, Vector3 startPos, Vector3 endPos, float startTime)
        {
            base.Init(piece, startPos, endPos, startTime);
            var newActiveTime = (_startPos - _endPos).magnitude / _distanceBySecond;
            _activeTime = Mathf.Min(newActiveTime, _activeTime);
        }

        public override void MoveByTimeline(float time, bool isSimulation = false)
        {
            base.MoveByTimeline(time, isSimulation);

            var currentTime = time - _startTime;
            var progress = (float)currentTime / _activeTime;
            transform.position = GetPositionByProgress(_startPos, _endPos, progress);
        }

        public Vector3 GetPositionByProgress(Vector3 startPos, Vector3 endPos, float progress)
        {
            var currentPos = Vector3.Lerp(startPos, endPos, progress);
            currentPos.y += _arcSize * _positionCurve.Evaluate(progress);
            return currentPos;
        }

        public override void NextMove()
        {
        }
    }
}

