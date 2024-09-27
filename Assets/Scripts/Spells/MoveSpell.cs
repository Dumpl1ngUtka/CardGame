using Battleground;
using UnityEngine;
using UnityEngine.AI;
using UI.Marker;

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
        private float[] _pathTimeArray;
        private Vector3[] _finalPath;

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
                _finalPath = new Vector3[Path.Length + 1];
                Path.CopyTo(_finalPath, 1);
                _finalPath[0] = _startPosition;
                _endRotate = (_endPosition - _startPosition);

                CalculatePathParameters(_finalPath, out float[] timeArray, out float distance);
                _pathTimeArray = timeArray;
                ActionTime = distance / _distancePerSecond;
                _rotationTime = Vector3.Angle(_startRotate, _endRotate) / _rotationSpeed;

                StartTime = Piece.Player.Timeline.GetTime;
                if (Piece.AddActivity(this))
                    IsSpellFinished = true;
                else
                    Debug.Log("Ќедостаточно времени на выполнение заклинани€");
            }
        }

        private void CalculatePathParameters(Vector3[] path, out float[] timeArray, out float distance)
        {
            timeArray = new float[path.Length];
            distance = 0f;
            for (int i = 0; i < path.Length - 1; i++)
            {
                timeArray[i] = distance / _distancePerSecond;
                var dist = Vector3.Distance(path[i], path[i + 1]);
                distance += dist;
            }
            timeArray[path.Length - 1] = distance / _distancePerSecond;
        }

        //private Vector3 SmoothPath(Vector3[] originPath)
        //{
        //    BezierCurve 
        //}

        public override void RightMouseClick(RaycastHit hit)
        {
            
        }

        public override void Release(float time)
        {
            //if (time <= _rotationTime)
            //    Piece.transform.forward = Vector3.Lerp(_startRotate, _endRotate, time / _rotationTime);


            Piece.Animator.Play("Walk");
            Piece.Animator.SetFloat("Progress", time);
            for (int i = 1; i < _pathTimeArray.Length; i++)
            {
                if (_pathTimeArray[i] >= time)
                {
                    var progress = (time - _pathTimeArray[i-1])/(_pathTimeArray[i] - _pathTimeArray[i-1]);
                    //Debug.Log("time =" + time + " V1 = " + _pathTimeArray[i - 1] + " V2 = " + _pathTimeArray[i] + "Pr =" +progress);
                    Piece.transform.position = Vector3.Lerp(_finalPath[i - 1], _finalPath[i], progress);
                    break;
                }
            }
        }

        public override void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (_agent.enabled)
                    _agent.destination = hit.point;
            }
        }

        public override void StartRelease()
        {
            _startPosition = Piece.transform.position;
            _marker = Instantiate(MarkerPrefab) as MoveMarker;
            _marker.Init(this);
            _agent = Piece.Agent;
            _agent.enabled = true;
        }

        public override void EndRelease()
        {
            Destroy(_marker.gameObject);
            _agent.enabled = false;
        }
    }
}