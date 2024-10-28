using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public class PieceMover : MonoBehaviour
    {
        [SerializeField] private Piece _piece;
        [SerializeField] private Rigidbody _rigidbody;
        private float _speed = 0f;
        private float _currentMaxSpeed = 0f;
        public float _acceleration = 1f;
        private Vector3 _targetPosititon;
        private float _maxRotationSpeed = 60f;
        private float _maxSpeed => _piece.Attributes.MoveSpeed;
        public Rigidbody Rigidbody => _rigidbody;

        public void SetMoveTarget(Vector3 targetPostition)
        {
            _targetPosititon = targetPostition;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            SetSpeed();
            Rotate();
        }

        private void SetSpeed()
        {
            var delta = Vector3.Angle(transform.forward, _targetPosititon - transform.position);
            if (delta < 25)
                _currentMaxSpeed = _maxSpeed;
            else
                _currentMaxSpeed = Mathf.Lerp(_maxSpeed, 0, (delta - 25) / 180);

            _speed = Mathf.MoveTowards(_speed, _currentMaxSpeed, _acceleration * Time.deltaTime);
        }

        private void Move()
        {
            _rigidbody.velocity = transform.forward * _speed;
        }

        public void Rotate()
        {
            var delta = Vector3.SignedAngle(transform.forward, _targetPosititon-transform.position, Vector3.up);
            delta = Mathf.Clamp(delta, -_maxRotationSpeed, _maxRotationSpeed) * Time.deltaTime;
            var newPivotRotation = transform.rotation.eulerAngles + new Vector3(0, delta, 0);
            transform.rotation = Quaternion.Euler(newPivotRotation);
        }
    }

}

