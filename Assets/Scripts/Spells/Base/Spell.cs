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

        public bool IsSpellReleased { get; protected set; }
        public Piece Piece { get; protected set; }

        public virtual void Init(Piece piece) 
        {
            Piece = piece;
            IsSpellReleased = false;
        }

        public virtual void StartRelease()
        {
        }

        public virtual void Update()
        {

        }

        public virtual void EndRelease()
        {
            if (IsSpellReleased)
                Destroy(this);
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

