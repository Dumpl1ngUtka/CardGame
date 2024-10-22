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
        public Rigidbody Rigidbody { get; private set; }
        public PieceStateMachine StateMachine { get; private set; }

        #region Spells
        private List<Spell> MoveSpells;
        private List<Spell> AttackSpells;
        private List<Spell> HealSpells;
        private List<Spell> DefenceSpells;

        #endregion

        #region CameraPivot
        public Vector3 PivotPosition => transform.position;
        public Transform PivotTransform => transform;
        #endregion

        #region WeightPoint

        public Transform Transform => transform;
        public Vector3 Position => transform.position;

        public float TeamID => Player.TeamID;

        public float DangerWeight => 0;

        public float ChargedSkillsDamage
        {
            get
            {
                var damage = 0f;
                foreach (var spell in AttackSpells)
                {
                    var attackSpell = spell as IAttackSpell;
                    if (spell.IsSpellReady)
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
                foreach (var spell in AttackSpells)
                {
                    var attackSpell = spell as IAttackSpell;
                    maxDPS += attackSpell.Damage / (spell.ActionTime + spell.Cooldown);
                }
                return maxDPS;
            }
        }

        public float MissingHealth => Health.MaxHealth - Health.CurrentHealth;

        public float CurrentHealth => Health.CurrentHealth;
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
            Rigidbody = GetComponent<Rigidbody>();

            StateMachine = new PieceStateMachine(this);
        }

        private void Update()
        {
            StateMachine.Update();
        }

        private void SetAvailableSkills()
        {
            MoveSpells = new List<Spell>();
            AttackSpells = new List<Spell>();
            HealSpells = new List<Spell>();
            DefenceSpells = new List<Spell>();
            foreach (var spell in Unit.Inventory.GetSpells())
            {
                if (spell is IMoveSpell)
                    MoveSpells.Add(spell);
                if (spell is IAttackSpell)
                    AttackSpells.Add(spell);
                if (spell is IHealSpell)
                    HealSpells.Add(spell);
                if (spell is IDefenceSpell)
                    DefenceSpells.Add(spell);
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

        public void MoveTo(Vector3 target)
        {
            var direction = (target - transform.position).normalized;
            var velocity = 100 * Time.deltaTime * direction;
            //velocity.y -= 9.81f;
            Rigidbody.velocity = velocity;
        }

        public void Stop()
        {
            Rigidbody.velocity = Vector3.zero;
        }
    }
 }



