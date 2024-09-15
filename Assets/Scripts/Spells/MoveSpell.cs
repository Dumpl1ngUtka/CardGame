using Battleground;
using UnityEngine;
using System.Reflection;
using UnityEditor.Animations;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/MoveSpell")]
    public class MoveSpell : Spell
    {
        [SerializeField] private float _distancePerSecond;

        //Delte this
        [SerializeField] private RuntimeAnimatorController _controller;

        private Vector3 _endPosition;
        private Vector3 _startPosition;
        private Vector3 _startRotate;
        private Vector3 _endRotate;
        private float _rotationTime;
        private float _rotationSpeed = 90;

        public override void RemoveFromTimeline()
        {

        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                _startPosition = Piece.transform.position;
                _endPosition = hit.point;

                _startRotate = Piece.transform.forward;
                _endRotate = (_endPosition - _startPosition);

                ActionTime = (_startPosition - _endPosition).magnitude / _distancePerSecond;
                _rotationTime = Vector3.Angle(_startRotate, _endRotate) / _rotationSpeed;

                StartTime = Piece.Player.Timeline.GetTime;
                if (Piece.AddActivity(this))
                    IsSpellFinished = true;
                else
                    Debug.Log("Ќедостаточно времени на выполнение заклинани€");
            }
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }

        public override void Release(float time)
        {
            if (time <= _rotationTime)
                Piece.transform.forward = Vector3.Lerp(_startRotate, _endRotate, time / _rotationTime);


            Piece.Animator.Play("Walk");
            Piece.Animator.SetFloat("Progress", time);
            Piece.transform.position = Vector3.Lerp(_startPosition, _endPosition, time / ActionTime);
        }

        public override void Update()
        {
        }

        public override void StartRelease()
        {
        }

        public override void EndRelease()
        {
            //throw new System.NotImplementedException();
        }
    }
}