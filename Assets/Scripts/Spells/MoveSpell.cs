using Battleground;
using UnityEngine;
using System.Reflection;
using UnityEditor.Animations;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;
using UI.Marker;
using System.Drawing;
using System.IO;

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
        private NavMeshAgent _agent; 
        private MoveMarker _marker;
        public Vector3[] Path => _agent.path.corners; 

        public override void RemoveFromTimeline()
        {

        }

        public override void LeftMouseClick(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Piece>() == null)
            {
                _endPosition = hit.point;

                _startRotate = Piece.transform.forward;
                _endRotate = (_endPosition - _startPosition);


                ActionTime = CalculatePathDistance(Path) / _distancePerSecond;
                _rotationTime = Vector3.Angle(_startRotate, _endRotate) / _rotationSpeed;

                StartTime = Piece.Player.Timeline.GetTime;
                if (Piece.AddActivity(this))
                    IsSpellFinished = true;
                else
                    Debug.Log("Ќедостаточно времени на выполнение заклинани€");
            }
        }

        private float CalculatePathDistance(Vector3[] path)
        {
            var distance = 0f;
            for (int i = 0; i < path.Length - 1; i++)
            {
                distance += Vector3.Distance(path[i], path[i + 1]);
            }
            return distance;
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }

        public override void Release(float time)
        {
            //if (time <= _rotationTime)
            //    Piece.transform.forward = Vector3.Lerp(_startRotate, _endRotate, time / _rotationTime);


            Piece.Animator.Play("Walk");
            Piece.Animator.SetFloat("Progress", time);
            //Piece.transform.position = 
            Piece.transform.position = Vector3.Lerp(_startPosition, _endPosition, time / ActionTime);
        }

        public override void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                _agent.destination = hit.point;
            }
        }

        public override void StartRelease()
        {
            _startPosition = Piece.transform.position;
            _marker = Instantiate(MarkerPrefab) as MoveMarker;
            _marker.Init(this);
            _agent = Piece.Agent;
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);
        }
    }
}