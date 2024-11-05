using AI;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground
{
    public class PieceStateMachine : MonoBehaviour, IAIWeightPoint
    {
        [SerializeField] private PieceState[] _availableStates;
        private PieceState _currentState;
        public List<PieceState> TransitionStates;
        public Piece Piece { get; private set; }
        public SituationAnalyzer SituationAnalyzer {get; private set;}

        #region WeightPoint

        public Transform Transform => Piece.transform;
        public Vector3 Position => Piece.transform.position;
        public int TeamID => Piece.Player.TeamID;
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
                foreach (var ability in DamageAbilites)
                {
                    var attackSpell = ability as IDamageAbility;
                    maxDPS += attackSpell.DPM;
                }
                return maxDPS;
            }
        }
        public float MissingHealth => Piece.Health.MaxHealth - Piece.Health.CurrentHealth;
        public float CurrentHealth => Piece.Health.CurrentHealth;
        public List<IAIWeightPoint> Group => SituationAnalyzer.GetAlliesPoints();
        #endregion

        #region Abilites
        public List<PieceAbility> MoveAbilites { get; private set; }
        public List<PieceAbility> DamageAbilites { get; private set; }
        public List<PieceAbility> HealAbilites { get; private set; }
        public List<PieceAbility> BuffAbilites { get; private set; }

        #endregion

        public void Init(Piece piece)
        {
            Piece = piece;
            SituationAnalyzer = new SituationAnalyzer(this);
            TransitionStates = new List<PieceState>()
            {
                new WalkAlone(this),
                new RunAway(this),
                new FollowToEnemy(this),
            };
            ChangeState(TransitionStates[0]);
            Piece.Unit.Inventory.InventoryChanged += SetAvailableSkills;
            SetAvailableSkills();
        }

        public void Update()
        {
            _currentState.Update();
            SituationAnalyzer.Update();
        }

        public void ChangeState(PieceState state)
        {
            // Debug.Log(state);
            state?.Exit();
            _currentState = state;
            state?.Enter();
        }

        private void SetAvailableSkills()
        {
            MoveAbilites = new List<PieceAbility>();
            DamageAbilites = new List<PieceAbility>();
            HealAbilites = new List<PieceAbility>();
            BuffAbilites = new List<PieceAbility>();
            foreach (var ability in Piece.Unit.GetAbilityArray())
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
    }

}
