using System;
using UnityEngine;

namespace Units
{
    [Serializable]
    public struct UnitResists
    {
        public float FireResist
        {
            get { return Mathf.Clamp01(_fireResist); }
            set { _fireResist = value; }
        }
        public float PoisonResist
        {
            get { return Mathf.Clamp01(_poisonResist); }
            set { _poisonResist = value; }
        }
        public float IceResist
        {
            get { return Mathf.Clamp01(_iceResist); }
            set { _iceResist = value; }
        }
        public float WaterResist
        {
            get { return Mathf.Clamp01(_waterResist); }
            set { _waterResist = value; }
        }
        public float CurseResist
        {
            get { return Mathf.Clamp01(_curseResist); }
            set { _curseResist = value; }
        }
        public float FreezeResist
        {
            get { return Mathf.Clamp01(_freezeResist); }
            set { _freezeResist = value; }
        }
        public float StunResist
        {
            get { return Mathf.Clamp01(_stunResist); }
            set { _stunResist = value; }
        }

        [SerializeField, Range(0, 1)] private float _fireResist;
        [SerializeField, Range(0, 1)] private float _poisonResist;
        [SerializeField, Range(0, 1)] private float _iceResist;
        [SerializeField, Range(0, 1)] private float _waterResist;
        [SerializeField, Range(0, 1)] private float _curseResist;
        [SerializeField, Range(0, 1)] private float _freezeResist;
        [SerializeField, Range(0, 1)] private float _stunResist;
    }
}