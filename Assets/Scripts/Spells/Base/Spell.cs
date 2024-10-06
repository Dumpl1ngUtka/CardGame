using Battleground;
using UI.Marker;
using UnityEngine;


namespace Units
{
    public abstract class Spell : ScriptableObject, IObjectForInfoRenderer
    {
        public string Name;
        public LayerMask Mask;
        public SpellTypes Type;
        public Sprite MainBackground;
        public int Ñonsumption;
        public float ActionTime;
        public Marker MarkerPrefab;

        public Piece Piece { get; protected set; }
        public float StartTime { get; protected set; }
        public float EndTime => StartTime + ActionTime;

        public bool IsSpellFinished { get; protected set; }

        public virtual void Init(Piece piece) 
        {
            IsSpellFinished = false;
            Piece = piece;
        }

        public abstract void RemoveFromTimeline();

        public abstract void LeftMouseClick(RaycastHit hit);

        public abstract void RightMouseClick(RaycastHit hit);

        public abstract void Release(float time = 0);

        public abstract void Update();

        public abstract void StartRelease();
        public abstract void EndRelease();

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
            {
                Title = Name,
                UnderTitle = Ñonsumption.ToString(),
            };
        }
    }
}

