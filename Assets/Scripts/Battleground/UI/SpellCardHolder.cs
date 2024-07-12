using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Battleground.UI
{
    public class SpellCardHolder : MonoBehaviour
    {
        [SerializeField] private SpellCard _spellCardPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Spell[] _spells;
        [SerializeField] private Transform[] _spawnPositions;

        private void Awake()
        {
            RenderCards(_spells);
        }

        public void RenderCards(Spell[] spells)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var index = (_spawnPositions.Length - _spells.Length)/2;

            foreach (Spell spell in spells)
            {
                var spellCard = Instantiate(_spellCardPrefab, _spawnPositions[index].position, _spawnPositions[index].rotation, _container);
                spellCard.Init(spell);
                index++;
            }
        }
    }
}
