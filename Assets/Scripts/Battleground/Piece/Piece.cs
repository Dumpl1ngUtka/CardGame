using AI;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForInfoRenderer, IDamageable, ICameraPivot
    {
        public Animator Animator;
        public NavMeshAgent Agent { get; private set; }
        public PieceClothesController Clothes { get; private set; }
        public PieceAttributes Attributes { get; private set; }
        public PieceUIRenderer UI { get; private set; }
        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }
        public Player Player { get; private set; }
        public PieceStateMachine StateMachine { get; private set; }
        public PieceMover PieceMover { get; private set; }

        #region CameraPivot
        public Vector3 PivotPosition => transform.position;
        public Transform PivotTransform => transform;
        #endregion

        public void Init(Unit unit, Player player)
        {
            Unit = unit;
            Player = player;
            Agent = GetComponent<NavMeshAgent>();
            UI = GetComponent<PieceUIRenderer>();

            Clothes = GetComponent<PieceClothesController>();
            Clothes.Init(unit.Inventory);

            Attributes = new(this);
            Health = new(Attributes);
            Health.Died += Died;

            PieceMover = GetComponent<PieceMover>();
            StateMachine = GetComponent<PieceStateMachine>();
            StateMachine.Init(this);
        }


        private void Died()
        {
        }

        public InfoForInfoRenderer GetInfo()
        {
            var info = Unit.GetInfo();
            info.HealthBarFill = Health.HealthFill;
            return info;
        }

        private void OnDisable()
        {
            Health.Died -= Died;
        }

        public void ApplyDamage(Damage damage)
        {
            Health.ApplyDamage(damage.Value);
        }

        public void MoveTo(Vector3 target) => PieceMover.SetMoveTarget(target);
    }
 }



