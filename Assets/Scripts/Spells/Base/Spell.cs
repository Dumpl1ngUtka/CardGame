using Battleground;
using UnityEngine;


namespace Units
{
    public abstract class Spell : ScriptableObject, IObjectForInfoRenderer
    {
        [Header("Visual")]
        public string Name;
        public Sprite MainBackground;
        [Header("Time Values")]
        public float ActionTime;
        public float Cooldown;
        [Header("Other")]
        private float _currentCooldownTime;
        public Piece Piece { get; protected set; }
        public bool IsSpellReady => CurrentCooldownTime < 0;
        public float CurrentCooldownTime => _currentCooldownTime;

        public virtual void Init(Piece piece) 
        {
            Piece = piece;
        }

        public virtual void Update()
        {
            if (!IsSpellReady)
                _currentCooldownTime -= Time.deltaTime;
        }

        public virtual void StartRelease()
        {

        }

        public virtual void EndRelease()
        {
            _currentCooldownTime = Cooldown;
        }

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
            {
                Title = Name,
            };
        }
    }

    public interface IAttackSpell
    {
        public float Damage { get; }
    }

    public interface IHealSpell
    {
        public float Heal { get; }
    }

    public interface IDefenceSpell
    {
        
    }

    public interface IMoveSpell
    {

    }
}

