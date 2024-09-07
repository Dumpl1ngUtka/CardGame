using Battleground;
using UnityEngine;

namespace Units
{
    public class Fireball : MonoBehaviour, IMoveByTimeline
    {
        [SerializeField] private float _distanceByStep;
        [SerializeField] private float _arcSize;
        [SerializeField] private Collider _hitbox;
        [SerializeField] private AnimationCurve _positionCurve;
        [SerializeField] private GameObject _head;
        private Player _player;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private int _startIndex;
        private int _stepCount;

        public void Init(Player player, Vector3 startPos, Vector3 endPos, int startIndex)
        {
            _player = player;
            _startPos = startPos;
            _endPos = endPos;
            _startIndex = startIndex;
            _stepCount = (int)((_startPos - _endPos).magnitude / _distanceByStep);
            _player.Timeline.OnValueChanged += MoveByTimeline;
            MoveByTimeline(0);
        }

        public void MoveByTimeline(float index)
        {
            var currentIndex = index - _startIndex;
            if (currentIndex < 0)
            {
                SetHide(true);
                return;
            }
            SetHide(false);
            var progress = (float)currentIndex / _stepCount;
            transform.position = GetPositionByProgress(_startPos, _endPos, progress);
        }

        public Vector3 GetPositionByProgress(Vector3 startPos, Vector3 endPos, float progress)
        {
            var currentPos = Vector3.Lerp(startPos, endPos, progress);
            currentPos.y += _arcSize * _positionCurve.Evaluate(progress);
            return currentPos;
        }

        private void SetHide(bool isHide)
        {
            _hitbox.enabled = !isHide;
            GetComponent<Renderer>().enabled = !isHide;
        }

        private void OnDisable()
        {
            _player.Timeline.OnValueChanged -= MoveByTimeline;
        }

        public void NextMove()
        {
            throw new System.NotImplementedException();
        }

        //private IEnumerator CollisionCalculations()
        //{
        //    var radius = (_hitbox as SphereCollider).radius;
        //    for (int i = 0; i < _stepCount * 2; i++)
        //    {
        //        var pos = GetPositionByProgress(_startPos, _endPos, (float)i / _stepCount);
        //        if (Physics.SphereCast(pos, radius, Vector3.one, out RaycastHit hit))
        //        {
        //            hit.collider.TryGetComponent(out Piece ounPiece);
        //            if (ounPiece != null && ounPiece.Player == _player)
        //                continue;
        //            Debug.Log(i);
        //            _collideIndex = i / 2;
        //            break;
        //        }
        //    }
        //    yield return null;

        //}
    }
}

