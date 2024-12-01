using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class RunAway : PieceState
    {
        private const float _updateTime = 0.1f;
        private const int _raycastAmount = 10;
        private float _timer;
        private Vector3 _previousDirection = Vector3.zero;
        private List<PieceAbility> _availableAbilities;

        protected override float MinStateTime => 3;
        protected override float MaxStateTime => float.PositiveInfinity;

        protected override List<PieceAbility> AvailableAbilityList => _availableAbilities;

        public override IAIWeightPoint Target => SituationAnalyzer.ClosestEnemy;

        public override void Enter(PieceState previousState)
        {
            base.Enter(previousState);
            _availableAbilities = StateMachine.MoveAbilites.Cast<PieceAbility>().ToList();
            Piece.UI.ChangeGroundIndicator(Color.green);
        }


        public override void Update()
        {
            base.Update();
            if (_timer < _updateTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _timer = 0f;
                var movePoint = ChooseDirectionByRaycast(_raycastAmount) ;
                Piece.MoveTo((movePoint - Piece.transform.position).normalized);
                _previousDirection = (movePoint - Piece.transform.position).normalized;
            }
        }

        private Vector3 CalculateBestDirection()
        {
            Vector3 bestDirection = Vector3.zero;
            foreach (var enemyPoint in SituationAnalyzer.EnemyPoints)
            {
                var direction =  enemyPoint.Position - SituationAnalyzer.SelfWeight.Position;
                bestDirection -= direction.normalized * enemyPoint.DangerWeight / (Mathf.Pow(direction.magnitude, 2));
            }

            return bestDirection.normalized;
        }

        Vector3 ChooseDirectionByRaycast(int raycastAmount)
        {
            Vector3 resultTarget = Vector3.zero;
            float maxDot = float.NegativeInfinity;
            List<Vector3> points = new List<Vector3>();

            float stepSize = 360 / raycastAmount;
            for (float i = 0; i <= 360; i += stepSize)
            {
                Vector3 rayResult = Quaternion.AngleAxis(i, Piece.transform.up) * Piece.transform.forward * 6;
                Vector3 rayCastStartPoint = Piece.transform.position + rayResult + Vector3.up;
                if (!Physics.Raycast(Piece.transform.position + Vector3.up, rayResult, out RaycastHit _, 6, Piece.MapLayers))
                {
                    if (Physics.Raycast(rayCastStartPoint, -Piece.transform.up, out RaycastHit hitVertical, 3, Piece.MapLayers))
                    {
                        if (NavMesh.SamplePosition(hitVertical.point, out NavMeshHit navMeshHit, 0.5f, NavMesh.AllAreas))
                        {
                            Vector3 hitDirection = (navMeshHit.position - Piece.transform.position).normalized;
                            Vector3 bestDirection = CalculateBestDirection();
                            float hitDirectionDot = Vector3.Dot(hitDirection, bestDirection);
                            points.Add(navMeshHit.position);
                            if (hitDirectionDot > maxDot)
                            {
                                maxDot = hitDirectionDot;
                                resultTarget = navMeshHit.position;
                            }
                        }
                    }
                }
            }
            Debug.DrawLine(Piece.transform.position + Vector3.up, resultTarget);
            return resultTarget;
            if ((maxDot < 0.65f) && (resultTarget != Vector3.zero))
            {
                Debug.Log("ASDAda");

                float maxDotResult = float.NegativeInfinity;
                Vector3 bestDirection = CalculateBestDirection();
                foreach (Vector3 point in points)
                {
                    Vector3 hitDirection = (point - Piece.transform.position).normalized;
                    float dotForPreviousDirection = Vector3.Dot(hitDirection, _previousDirection);
                    float dotForBest = Vector3.Dot(hitDirection, bestDirection);
                    float resultDot = dotForBest + dotForPreviousDirection * 5;
                    if (resultDot > maxDotResult)
                    {
                        maxDotResult = resultDot;
                        resultTarget = point;
                    }
                }
            }
        }

        public override float GetMetric(SituationAnalyzer situationAnalyzer)
        {
            var metrix = 0f;
            if (SituationAnalyzer.StrongestEnemy != null)
            {
                metrix = SituationAnalyzer.StrongestEnemy.DangerWeight / SelfGroupWeight.DangerWeight;
            }
            return metrix;
        }
    }
}