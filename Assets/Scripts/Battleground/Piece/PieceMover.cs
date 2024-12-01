using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class PieceMover : MonoBehaviour
    {
        [SerializeField] private Piece _piece;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _centerOfMass;
        [SerializeField] private LayerMask _groundLayers;
        private float _rotationFraction = 0f;
        private float _speed = 0f;
        private float _currentMaxSpeed = 0f;
        public float _acceleration = 1f;
        private Vector3 _targetPosititon;
        private float _maxRotationSpeed = 120f;
        private float _maxSpeed => _piece.Attributes.MoveSpeed;
        private bool _isNeedToMove => Direction != Vector3.zero;
        private bool _isNeedToRotate => _isNeedToMove || RotationDirection != Vector3.zero;
        public Rigidbody Rigidbody => _rigidbody;

        #region Direction
        private Vector3 _direction;
        private float _directionTimer;
        private Vector3 Direction
        {
            get { return _direction; }
            set 
            {
                _directionTimer = 0.5f;
                _direction = value; 
            }
        }

        private Vector3 RotationDirection;
        #endregion

        public float SpeedFraction => _speed / _maxSpeed;
        public float RotationFraction => _rotationFraction;

        private void Awake()
        {
            _rigidbody.centerOfMass = _centerOfMass.position;
            _targetPosititon = transform.position;
        }

        public void SetMoveDirection(Vector3 directoion)
        {
            Direction = directoion;
        }

        public void SetRotationDirection(Vector3 directoion)
        {
            RotationDirection = directoion;
        }

        public void SetMoveTarget(Vector3 targetPostition)
        {
            _targetPosititon = targetPostition;
        }

        private void FixedUpdate()
        {
            if (_isNeedToMove)
                Move();
        }

        private void Update()
        {
            if (_directionTimer > 0f)
                _directionTimer -= Time.deltaTime;
            else
                Direction = Vector3.zero;

            if (_isNeedToRotate)
                Rotate();
            SetSpeed();
        }

        private void SetSpeed()
        {
            if (!_isNeedToMove)
                _currentMaxSpeed = 0f;

            var delta = Vector3.Angle(transform.forward, Direction);
            if (delta < 25)
                _currentMaxSpeed = _maxSpeed;
            else
                _currentMaxSpeed = Mathf.Lerp(_maxSpeed, 0, (delta - 25) / 90);

            _speed = Mathf.MoveTowards(_speed, _currentMaxSpeed, _acceleration * Time.deltaTime);
        }

        private void Move()
        {
            if (IsOnGround())
            {
                //if (_currentMaxSpeed - _rigidbody.velocity.magnitude > 1)
                //    _rigidbody.AddForce(transform.forward * _acceleration, ForceMode.VelocityChange);
                var moveVec = transform.forward * _speed;
                moveVec.y = _rigidbody.velocity.y;
                _rigidbody.velocity = moveVec;
            }
            //_piece.Animator.Pla
        }

        private void Rotate()
        {
            if (IsOnGround())
            {
                var delta = Vector3.SignedAngle(transform.forward, Direction, Vector3.up);
                delta = Mathf.Clamp(delta, -_maxRotationSpeed, _maxRotationSpeed);
                _rotationFraction = Mathf.Abs(delta / _maxRotationSpeed);
                var newPivotRotation = transform.rotation.eulerAngles + new Vector3(0, delta, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(newPivotRotation);
            }
        }

        private bool IsOnGround()
        {
            return Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.5f, _groundLayers);
        }
    }

}

