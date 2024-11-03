using AI;
using System.Xml.Serialization;
using UnityEngine;

namespace Battleground
{
    public abstract class PieceAbility : ScriptableObject, IObjectForInfoRenderer
    {
        #region Main Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _releaseTime;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isCanMoveWhileUse;
        #endregion

        private float _releaseTimer;
        private float _cooldownTimer = 0f;

        #region Properties
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float ReleaseTime => _releaseTime;
        public float Cooldown => _cooldown;
        public bool IsCanMoveWhileUse => _isCanMoveWhileUse;
        public bool IsReadyToUse => _cooldownTimer <= 0f && !IsUsed;
        public bool IsUsed => _releaseTimer > 0f;

        #endregion

        public InfoForInfoRenderer GetInfo()
        {
            return new InfoForInfoRenderer
            {
                Title = Name,
                UnderTitle = Description,
                ContentLine1 = "Release time = " + ReleaseTime,
                ContentLine2 = "Cooldown time = " + Cooldown,
            };
        }

        public abstract float GetMetric(SituationAnalyzer situationAnalyzer);

        public void StartRelease()
        {
            _releaseTimer = ReleaseTime;
            Debug.Log("START USE " + Name);
        }

        public void Update()
        {
            if (_releaseTimer > 0f)
            {
                Release();
                _releaseTimer -= Time.deltaTime;
            }

            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        protected abstract void Release();

        public void EndRelease()
        {
            Debug.Log("END USE " + Name);
            _cooldownTimer = Cooldown;
        }
    }
}
