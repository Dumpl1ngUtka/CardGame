using AI;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForInfoRenderer, IDamageable, ICameraPivot, IEffectHolder
    {
        public PieceAnimator Animator { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public PieceClothesController Clothes { get; private set; }
        public PieceAttributes Attributes { get; private set; }
        public PieceUIRenderer UI { get; private set; }
        public PieceHealth Health { get; private set; }
        public PieceEffectsHolder EffectsHolder { get; private set; }
        public Unit Unit { get; private set; }
        public Player Player { get; private set; }
        public PieceStateMachine StateMachine { get; private set; }
        public PieceMover PieceMover { get; private set; }
        public LayerMask MapLayers;

        #region CameraPivot
        public Vector3 PivotPosition => transform.position;
        public Transform PivotTransform => transform;
        #endregion

        public void Init(Unit unit, Player player)
        {
            Unit = unit;

            Player = player;
            Agent = GetComponent<NavMeshAgent>();

            EffectsHolder = new();
            EffectsHolder.Init(this);

            Attributes = new(this);
            Health = new(this);
            Health.Died += Died;

            UI = GetComponent<PieceUIRenderer>();
            UI.Init(this);

            Animator = GetComponent<PieceAnimator>();
            Animator.Init();

            Clothes = GetComponent<PieceClothesController>();
            Clothes.Init(unit.Inventory);

            PieceMover = GetComponent<PieceMover>();
            StateMachine = GetComponent<PieceStateMachine>();
            StateMachine.Init(this);
        }


        private void Died()
        {
            Destroy(gameObject);
        }

        public InfoForInfoRenderer GetInfo()
        {
            var info = new InfoForInfoRenderer
            {
                HealthBarFill = Health.HealthFill
            };
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

        public void MoveTo(Vector3 direction) => PieceMover.SetMoveDirection(direction);
        public void LookTo(Vector3 direction) => PieceMover.SetRotationDirection(direction);
    }
 }



