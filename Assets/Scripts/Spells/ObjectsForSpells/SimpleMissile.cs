using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class SimpleMissile : SpellObject, ICameraPivot
    {
        [SerializeField] private float _distanceBySecond;
        [SerializeField] private float _arcSize;
        [SerializeField] private AnimationCurve _positionCurve;
        private List<IDamageable> _collidedObjects;
        private Vector3 _startPos;
        private Vector3 _endPos;

        public Vector3 PivotPosition => transform.position;
        public Transform PivotTransform => transform;

        public void Init(Piece piece, Vector3 startPos, Vector3 endPos, float startTime)
        {
            base.Init(piece, startTime);
            _startPos = startPos;
            _endPos = endPos;
            _collidedObjects = new List<IDamageable>();
            var newActiveTime = (_startPos - _endPos).magnitude / _distanceBySecond;
            _activeTime = Mathf.Min(newActiveTime, _activeTime);
        }

        public override void MoveByTimeline(float time, bool isSimulation = false)
        {
            base.MoveByTimeline(time, isSimulation);

            var currentTime = time - _startTime;
            var progress = (float)currentTime / _activeTime;
            transform.position = GetPositionByProgress(_startPos, _endPos, progress);

            if (isSimulation)
                if (_collidedObjects.Count > 0)
                {
                    foreach (var collidedObject in _collidedObjects)
                        collidedObject.ApplyDamage(new Damage(_damage, collidedObject, _piece));
                    Destroy(gameObject);
                }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable obj) && _collidedObjects.Contains(obj))
                _collidedObjects.Remove(obj);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable obj) && !_collidedObjects.Contains(obj))
                _collidedObjects.Add(obj);
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

