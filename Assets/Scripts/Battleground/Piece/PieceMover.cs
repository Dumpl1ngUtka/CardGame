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
        private bool _isNeedToMove => Vector3.Distance(_targetPosititon, transform.position) > 0.1f;
        public Rigidbody Rigidbody => _rigidbody;


        private void Awake()
        {
            _rigidbody.centerOfMass = _centerOfMass.position;
            _targetPosititon = transform.position;
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
            if (_isNeedToMove)
                Rotate();
            SetSpeed();
        }

        private void SetSpeed()
        {
            if (!_isNeedToMove)
                _currentMaxSpeed = 0f;

            var delta = Vector3.Angle(transform.forward, _targetPosititon - transform.position);
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
        }

        public void Rotate()
        {
            if (IsOnGround())
            {
                var delta = Vector3.SignedAngle(transform.forward, _targetPosititon - transform.position, Vector3.up);
                delta = Mathf.Clamp(delta, -_maxRotationSpeed, _maxRotationSpeed) * Time.deltaTime;
                var newPivotRotation = transform.rotation.eulerAngles + new Vector3(0, delta, 0);
                transform.rotation = Quaternion.Euler(newPivotRotation);
            }

        }

        public void Stop()
        {
            _targetPosititon = transform.position;
        }

        private void WakeUp()
        {
            //var newRot = -transform.localEulerAngles * Time.fixedDeltaTime;
            //newRot.y = 0;
            //_rigidbody.MoveRotation(Quaternion.Euler(newRot));
        }

        private bool IsOnGround()
        {
            return Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, 0.5f, _groundLayers);
        }
    }

}

