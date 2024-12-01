//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.AI;

//namespace Battleground
//{
//    public class DuckCowardRun : PieceState
//    {
//        private const float _updateTime = 0.5f;
//        private float _timer;
//        [SerializeField] float _viewRadius;
//        [SerializeField] LayerMask _dangerLayers;
//        RaycastHit[] hits = new RaycastHit[30];
//        int currentDangerLVL = 0;
//        List<DangerForDuck> dangers = new List<DangerForDuck>();
//        NavMeshAgent agent;

//        [Range(6, 36)]
//        [SerializeField] int raysAmount;
//        [SerializeField] float raysUpdateInterval;
//        [SerializeField] LayerMask raysMask;

//        bool isInDanger = false;
//        Vector3 previousDirection = Vector3.zero;
//        private void Start()
//        {
//            agent = GetComponent<NavMeshAgent>();
//            StartCoroutine(DirectionChangeTimer(raysUpdateInterval));
//        }
//        public override void Update()
//        {
//            base.Update();
//            if (_timer < _updateTime)
//            {
//                _timer += Time.deltaTime;
//            }
//            else
//            {
//                _timer = 0f;
//                Piece.MoveTo(GetMoveVector());
//            }
//            isInDanger = FindDangers();
//        }
//        DangerData[] CalculateDangerDatas(List<DangerForDuck> dangersList)
//        {
//            DangerData[] dangerDatas = new DangerData[dangersList.Count];
//            for (int i = 0; i < dangersList.Count; i++)
//            {
//                dangerDatas[i].dangerLevel = dangersList[i].DangerLevel();
//                dangerDatas[i].distance = (dangersList[i].transform.position - transform.position).magnitude;
//                dangerDatas[i].direction = (dangersList[i].transform.position - transform.position).normalized;
//            }
//            return dangerDatas;
//        }
//        struct DangerData
//        {
//            public int dangerLevel;
//            public float distance;
//            public Vector3 direction;
//        }
//        Vector3 CalculateBestDirection(DangerData[] dangerDatalist)
//        {
//            Vector3 bestDirection = Vector3.zero;
//            foreach (DangerData dangerData in dangerDatalist)
//            {
//                bestDirection -= dangerData.direction * dangerData.dangerLevel / (Mathf.Pow(dangerData.distance, 2));
//            }
//            bestDirection = bestDirection.normalized;

//            return bestDirection;
//        }

//        bool FindDangers()
//        {
//            dangers.Clear();
//            currentDangerLVL = 0;
//            if (Physics.SphereCastNonAlloc(transform.position, _viewRadius, transform.forward, hits, _viewRadius, _dangerLayers) > 0)
//            {
//                foreach (RaycastHit hit in hits)
//                {
//                    try
//                    {
//                        if (hit.collider.gameObject.TryGetComponent<DangerForDuck>(out DangerForDuck danger))
//                        {
//                            currentDangerLVL += danger.DangerLevel();
//                            dangers.Add(danger);
//                        }
//                    }
//                    catch
//                    {
//                    }
//                }
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        Vector3 ChooseDirectionByRaycast(int raycastAmount)
//        {
//            Vector3 resultTarget = Vector3.zero;
//            float maxDot = float.NegativeInfinity;
//            List<Vector3> points = new List<Vector3>();

//            float stepSize = 360 / raycastAmount;
//            for (float i = 0; i <= 360; i += stepSize)
//            {
//                Vector3 rayResult = Quaternion.AngleAxis(i, transform.up) * transform.forward * 2;
//                Vector3 rayCastStartPoint = transform.position + rayResult;
//                if (!Physics.Raycast(transform.position, rayResult, out RaycastHit hitHorizontal, 2, raysMask))
//                {
//                    if (Physics.Raycast(rayCastStartPoint, -transform.up, out RaycastHit hitVertical, 3, raysMask))
//                    {
//                        if (NavMesh.SamplePosition(hitVertical.point, out NavMeshHit navMeshHit, 0.5f, NavMesh.AllAreas))
//                        {

//                            Vector3 hitDirection = (navMeshHit.position - transform.position).normalized;
//                            Vector3 bestDirection = CalculateBestDirection(CalculateDangerDatas(dangers));
//                            float hitDirectionDot = Vector3.Dot(hitDirection, bestDirection);
//                            points.Add(navMeshHit.position);
//                            if (hitDirectionDot > maxDot)
//                            {
//                                maxDot = hitDirectionDot;
//                                resultTarget = navMeshHit.position;
//                            }

//                        }
//                    }
//                }
//            }
//            if ((maxDot < 0.65f) && (resultTarget != Vector3.zero))
//            {
//                float maxDotResult = float.NegativeInfinity;
//                Vector3 bestDirection = CalculateBestDirection(CalculateDangerDatas(dangers));
//                foreach (Vector3 point in points)
//                {
//                    Vector3 hitDirection = (point - transform.position).normalized;
//                    float dotForPreviousDirection = Vector3.Dot(hitDirection, previousDirection);
//                    float dotForBest = Vector3.Dot(hitDirection, bestDirection);
//                    float resultDot = dotForBest + dotForPreviousDirection * 5;
//                    if (resultDot > maxDotResult)
//                    {
//                        maxDotResult = resultDot;
//                        resultTarget = point;
//                    }
//                }
//            }
//            return resultTarget;
//        }

//        IEnumerator DirectionChangeTimer(float interval)
//        {
//            while (true)
//            {
//                if (isInDanger)
//                {
//                    yield return new WaitForSeconds(interval);
//                    agent.destination = ChooseDirectionByRaycast(raysAmount);
//                    previousDirection = (agent.destination - transform.position).normalized;
//                }
//                else yield return null;
//            }
//        }
//    }

//}
