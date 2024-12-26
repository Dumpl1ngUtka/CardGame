using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    [CreateAssetMenu(menuName = "Config/Effects")]
    public class PieceEffect : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _isInfinite;
        [SerializeField, Min(0)] private float _duration;
        [SerializeField] private AdditionalPieceAttributes _pieceAttributes;
        [SerializeField] private List<EffectsCombination> _effectsCombinations;

        public string Name => _name;
        public float Duration => _duration;
        public bool IsInfinite => _isInfinite;
        public AdditionalPieceAttributes PieceAttributes => _pieceAttributes;
        public List<EffectsCombination> EffectsCombinations => _effectsCombinations;
        public bool HasEffectsCombination => EffectsCombinations.Count != 0;
    }

    [Serializable]
    public struct EffectsCombination
    {
        public PieceEffect AdditionalEffect;
        public PieceEffect ResultEffect;
    }
}