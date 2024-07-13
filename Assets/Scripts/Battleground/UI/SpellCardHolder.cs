using UnityEngine;

namespace Battleground.UI
{
    public class SpellCardHolder : MonoBehaviour
    {
        [SerializeField] private SpellCard _spellCardPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform[] _spawnPositions;

        public void RenderCards(Spell[] spells)
        {
            ClearContainer();

            var index = (_spawnPositions.Length - spells.Length)/2;

            foreach (Spell spell in spells)
            {
                var spellCard = Instantiate(_spellCardPrefab, _spawnPositions[index].position, _spawnPositions[index].rotation, _container);
                spellCard.Init(spell);
                index++;
            }
        }

        private void ClearContainer()
        {
            foreach (Transform child in _container.transform)
                Destroy(child.gameObject);
        }
    }
}
