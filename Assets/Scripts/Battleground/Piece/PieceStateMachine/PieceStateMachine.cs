using AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace Battleground
{
    public class PieceStateMachine : MonoBehaviour, IAIWeightPoint
    {
        private PieceState _currentState;
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

        public void Init(Piece piece)
        {
            Piece = piece;
            SituationAnalyzer = new SituationAnalyzer(this);
            InitTransitionStates();
            Piece.Unit.Inventory.InventoryChanged += SetAvailableSkills;
            SetAvailableSkills();
        }

        private void InitTransitionStates()
        {
            TransitionStates = new List<PieceState>()
            {
                ScriptableObject.CreateInstance<WalkAlone>(),
                ScriptableObject.CreateInstance<RunAway>(),
                ScriptableObject.CreateInstance<FollowToEnemy>(),
            };
            foreach (var state in TransitionStates)
                state.Init(this);

            ChangeState(TransitionStates[0]);
        }

        public void Update()
        {
            _currentState.Update();
            SituationAnalyzer.Update();
        }

        public void ChangeState(PieceState state)
        {
            //Debug.Log(state);
            _currentState?.Exit();
            state?.Enter(_currentState);
            _currentState = state;
        }

        private void SetAvailableSkills()
        {
            AllAbilites = Piece.Unit.GetAbilityArray().ToList();
            foreach (var ability in AllAbilites)
                ability.Init(this);

            MoveAbilites = AllAbilites.Where(AbilityType<IMoveAbility>).Cast<IMoveAbility>().ToList();
            DamageAbilites = AllAbilites.Where(AbilityType<IDamageAbility>).Cast<IDamageAbility>().ToList();
            HealAbilites = AllAbilites.Where(AbilityType<IHealAbility>).Cast<IHealAbility>().ToList();
            BuffAbilites = AllAbilites.Where(AbilityType<IBuffAbility>).Cast<IBuffAbility>().ToList();
        }

        private bool AbilityType<T>(PieceAbility ability)
        {
            if (ability is T)
                return true;
            return false;
        }

        public void ChargeAbility(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
