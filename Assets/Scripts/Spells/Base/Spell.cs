using Battleground;
using UnityEngine;


namespace Units
{
    public abstract class Spell : ScriptableObject, IObjectForUICard
    {
        public bool IsSpellReleased { get; protected set; }
        public Player Player { get; protected set; }

        #region CardUI
        public abstract Sprite Image { get; }
        public abstract Sprite TypeIcon { get; }
        public abstract Sprite ParamIcon1 { get; }
        public abstract Sprite ParamIcon2 { get; }
        public abstract string Title { get; }
        public abstract string ParamValue1 { get; }
        public abstract string ParamValue2 { get; }
        Spell IObjectForUICard.Spell => this;

        #endregion

        public virtual void Init(Player player) 
        {
            Player = player;
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

        }

        public virtual void LeftMouseClick(RaycastHit hit)
        {

        }

        public virtual void RightMouseClick(RaycastHit hit)
        {

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

