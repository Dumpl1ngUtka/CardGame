using UnityEngine;

namespace Battleground
{
    [RequireComponent(typeof(Figure))]
    public class FigureMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private float _minMoveDistance = 1f;
        private float _maxMoveDistance = 10f;
        private Vector3 _startPosition;

        private void Awake()
        {
            NewMove();
        }

        private void NewMove()
        {
            _startPosition = transform.position;
        }

        public void OnMouseDrag()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var newPosition = hit.point;
                newPosition.y = 0;
                if ((_startPosition - newPosition).magnitude > _maxMoveDistance)
                    newPosition = _startPosition + (newPosition - _startPosition).normalized * _maxMoveDistance;
                if ((newPosition - transform.position).magnitude >= _minMoveDistance )
                    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _speed);
            }
        }
    }
}

