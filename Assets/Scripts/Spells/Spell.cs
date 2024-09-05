using Battleground;
using System.Collections;
using UI.Marker;
using UnityEngine;


namespace Units
{
    public abstract class Spell : ScriptableObject, IObjectForCardRenderer
    {
        public string Name;
        public LayerMask Mask;
        public Sprite SpecializationIcon;
        public Sprite MainBackground;
        public int Ñonsumption;
        public int StepCount;
        public Marker MarkerPrefab;

        public Piece Piece { get; protected set; }
        public int StartIndex { get; protected set; }
        public int EndIndex => StartIndex + StepCount;

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

        public abstract void Start();

        public InfoForCardRenderer GetInfo()
        {
            return new InfoForCardRenderer
            {
                Title = Name,
                UnderTitle = Ñonsumption.ToString(),
            };
        }
    }
}

