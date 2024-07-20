using System.Collections;
using UnityEngine;

namespace Battleground
{
    [RequireComponent(typeof(Piece))]
    public class PieceMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private MovePoint _point;
        private float _maxMoveDistance = 10f;
        private Vector3 _startPosition;
        private Vector3 _rayHitPosition;
        private bool isMove = false;

        private void Awake()
        {
            NewMove();
        }

        private void NewMove()
        {
            _startPosition = transform.position;
        }

        private void OnMouseDown()
        {
            if (isMove)
                return;

            _point.gameObject.SetActive(true);
        }

        private void OnMouseDrag()
        {
            if (isMove)
                return;

            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                _rayHitPosition = hit.point;             
                _rayHitPosition.y = 0;

                var isMoreMaxDistance = (_startPosition - _rayHitPosition).magnitude > _maxMoveDistance;
                if (isMoreMaxDistance)
                    _rayHitPosition = _startPosition + (_rayHitPosition - _startPosition).normalized * _maxMoveDistance;

                _point.transform.position = _rayHitPosition;
            }
        }

        private void OnMouseUp()
        {
            if (isMove)
                return;

            if (_point.IsCanMoveHere)
                StartCoroutine(Move());

            _point.gameObject.SetActive(false);
        }

        private IEnumerator Move()
        {
            isMove = true;
            var moveTime = (transform.position - _rayHitPosition).magnitude / _speed;
            var timer = 0f;
            while (timer < moveTime)
            {
                transform.position = Vector3.Lerp(_startPosition, _rayHitPosition, timer / moveTime);
                timer += Time.deltaTime;
                yield return null;
            }
            _startPosition = transform.position;
            isMove = false;
        }
    }
}

