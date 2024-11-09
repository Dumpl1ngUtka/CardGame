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
        private float _speed = 0f;
        private float _currentMaxSpeed = 0f;
        public float _acceleration = 1f;
        private Vector3 _targetPosititon;
        private float _maxRotationSpeed = 60f;
        private float _maxSpeed => _piece.Attributes.MoveSpeed;
        private bool _isNeedToMove => Direction != Vector3.zero;
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
        #endregion

        private void Awake()
        {
            _rigidbody.centerOfMass = _centerOfMass.position;
            _targetPosititon = transform.position;
        }

        public void SetMoveDirection(Vector3 directoion)
        {
            Direction = directoion;
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

            if (_isNeedToMove)
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
                _piece.Animator.SetFloat("Speed", _speed / _maxSpeed);
                //if (_currentMaxSpeed - _rigidbody.velocity.magnitude > 1)
                //    _rigidbody.AddForce(transform.forward * _acceleration, ForceMode.VelocityChange);
                var moveVec = transform.forward * _speed;
                moveVec.y = _rigidbody.velocity.y;
                _rigidbody.velocity = moveVec;
            }
            //_piece.Animator.Pla
        }

        public void Rotate()
        {
            if (IsOnGround())
            {
                var delta = Vector3.SignedAngle(transform.forward, Direction, Vector3.up);
                _piece.Animator.SetFloat("RotationSpeed", Mathf.Abs(delta / _maxRotationSpeed));
                delta = Mathf.Clamp(delta, -_maxRotationSpeed, _maxRotationSpeed) * Time.deltaTime;
                var newPivotRotation = transform.rotation.eulerAngles + new Vector3(0, delta, 0);
                transform.rotation = Quaternion.Euler(newPivotRotation);
            }

        }

        private bool IsOnGround()
        {
            return Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.5f, _groundLayers);
        }
    }

}

