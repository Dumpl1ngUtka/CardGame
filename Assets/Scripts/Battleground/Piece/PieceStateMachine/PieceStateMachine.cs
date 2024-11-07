using AI;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace Battleground
{
    public class PieceStateMachine : MonoBehaviour, IAIWeightPoint
    {
        [SerializeField] private PieceState[] _availableStates;
        private PieceState _currentState;
        private PieceAbility _currentAbility;
        public List<PieceState> TransitionStates;
        public Piece Piece { get; private set; }
        public SituationAnalyzer SituationAnalyzer {get; private set;}

        #region WeightPoint

        public Transform Transform => Piece.transform;
        public Vector3 Position => Piece.transform.position;
        public int TeamID => Piece.Player.TeamID;
        public float DangerWeight => 0;
        public float ChargedSkillsDamage => DamageAbilites.Where(x => x.Ability.IsReadyToUse).Sum(x => x.Damage);
        public float DamagePerMinute => DamageAbilites.Sum(x => x.DPM);
        public float MissingHealth => Piece.Health.MaxHealth - Piece.Health.CurrentHealth;
        public float CurrentHealth => Piece.Health.CurrentHealth;
        public List<IAIWeightPoint> Group => SituationAnalyzer.GetAlliesPoints();
        #endregion

        #region Abilites
        public List<PieceAbility> AllAbilites { get; private set; }
        public List<IMoveAbility> MoveAbilites { get; private set; }
        public List<IDamageAbility> DamageAbilites { get; private set; }
        public List<IHealAbility> HealAbilites { get; private set; }
        public List<IBuffAbility> BuffAbilites { get; private set; }

        #endregion

        public bool IsAbilityUsed => _currentAbility != null;

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
            if (_currentAbility != null)
                _currentAbility.Update();
            else
                _currentState.Update();
            SituationAnalyzer.Update();
        }

        public void ChangeState(PieceState state)
        {
            //Debug.Log(state);
            state?.Exit();
            _currentState = state;
            state?.Enter();
        }

        private void SetAvailableSkills()
        {
            AllAbilites = Piece.Unit.GetAbilityArray().ToList();
            MoveAbilites = AllAbilites.Where(AbilityType<IMoveAbility>).Cast<IMoveAbility>().ToList();
            DamageAbilites = AllAbilites.Where(AbilityType<IDamageAbility>).Cast<IDamageAbility>().ToList();
            HealAbilites = AllAbilites.Where(AbilityType<IHealAbility>).Cast<IHealAbility>().ToList();
            BuffAbilites = AllAbilites.Where(AbilityType<IBuffAbility>).Cast<IBuffAbility>().ToList();
        }

        public void UseAbility(PieceAbility usedAbility)
        {
            Debug.Log("USE " + usedAbility.Name);
            _currentAbility = usedAbility;
            _currentAbility.ReleaseOver += RemoveAbility;
            _currentAbility.StartRelease(_currentState);
        }

        private void RemoveAbility()
        {
            _currentAbility.ReleaseOver -= RemoveAbility;
            StartCoroutine(_currentAbility.Charge());
            _currentAbility = null;
        }

        private bool AbilityType<T>(PieceAbility ability)
        {
            if (ability is T)
                return true;
            return false;
        }
    }
}
