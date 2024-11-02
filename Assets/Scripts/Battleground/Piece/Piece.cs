using AI;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Battleground
{
    public class Piece : MonoBehaviour, IObjectForInfoRenderer, IDamageable, ICameraPivot, IAIWeightPoint
    {
        public Animator Animator;
        public NavMeshAgent Agent { get; private set; }
        public PieceClothesController Clothes { get; private set; }
        public PieceAttributes Attributes { get; private set; }
        public PieceHealth Health { get; private set; }
        public Unit Unit { get; private set; }
        public Player Player { get; private set; }
        public PieceStateMachine StateMachine { get; private set; }
        public PieceMover PieceMover { get; private set; }

        #region Abilites
        private List<PieceAbility> MoveAbilites;
        private List<PieceAbility> DamageAbilites;
        private List<PieceAbility> HealAbilites;
        private List<PieceAbility> BuffAbilites;

        #endregion

        #region CameraPivot
        public Vector3 PivotPosition => transform.position;
        public Transform PivotTransform => transform;
        #endregion

        #region WeightPoint

        public Transform Transform => transform;
        public Vector3 Position => transform.position;
        public int TeamID => Player.TeamID;
        public float DangerWeight => 0;
        public float ChargedSkillsDamage
        {
            get
            {
                var damage = 0f;
                foreach (var spell in DamageAbilites)
                {
                    var attackSpell = spell as IAttackSpell;
                    if (spell.IsReadyToUse)
                        damage += attackSpell.Damage;
                }
                return damage;
            }
        }
        public float DamagePerMinute
        {
            get
            {
                var maxDPS = 0f;
                foreach (var spell in DamageAbilites)
                {
                    var attackSpell = spell as IDamageAbility;
                    maxDPS += attackSpell.DPM;
                }
                return maxDPS;
            }
        }
        public float MissingHealth => Health.MaxHealth - Health.CurrentHealth;
        public float CurrentHealth => Health.CurrentHealth;
        public List<IAIWeightPoint> Group => StateMachine.SituationAnalyzer.GetAlliesPoints();
        #endregion

        public void Init(Unit unit, Player player)
        {
            Unit = unit;
            Unit.Inventory.InventoryChanged += SetAvailableSkills;
            SetAvailableSkills();
            Agent = GetComponent<NavMeshAgent>();

            Clothes = GetComponent<PieceClothesController>();
            Clothes.Init(unit.Inventory);

            Attributes = new(this);
            Health = new(Attributes);
            Health.Died += Died;
            Player = player;

            PieceMover = GetComponent<PieceMover>();
            StateMachine = new PieceStateMachine(this);
        }

        private void Update()
        {
            StateMachine.Update();
        }

        private void SetAvailableSkills()
        {
            MoveAbilites = new List<PieceAbility>();
            DamageAbilites = new List<PieceAbility>();
            HealAbilites = new List<PieceAbility>();
            BuffAbilites = new List<PieceAbility>();
            foreach (var ability in Unit.Inventory.GetAbilites())
            {
                if (ability is IMoveAbility)
                    MoveAbilites.Add(ability);
                if (ability is IDamageAbility)
                    DamageAbilites.Add(ability);
                if (ability is IHealAbility)
                    HealAbilites.Add(ability);
                if (ability is IBuffAbility)
                    BuffAbilites.Add(ability);
            }
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
            Unit.Inventory.InventoryChanged -= SetAvailableSkills;
        }

        public void ApplyDamage(Damage damage)
        {
            Health.ApplyDamage(damage.Value);
        }

        public void MoveTo(Vector3 target) => PieceMover.SetMoveTarget(target);
    }
 }



