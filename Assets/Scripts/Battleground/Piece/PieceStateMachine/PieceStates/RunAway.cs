using AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class RunAway : PieceState
    {
        private const float _updateTime = 0.5f;
        private float _timer;
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
                Piece.MoveTo(GetMoveVector());
            }
        }

        private Vector3 GetMoveVector()
        {
            var moveVector = new Vector3();
            foreach (var enemyPoint in SituationAnalyzer.EnemyPoints)
            {
                moveVector += (SituationAnalyzer.SelfWeight.Position - enemyPoint.Position).normalized * enemyPoint.DamagePerMinute;
            }
            return moveVector;
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