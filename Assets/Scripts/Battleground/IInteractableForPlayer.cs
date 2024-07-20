using System.Collections;
using UnityEngine;

namespace Battleground
{
    public interface IInteractableForPlayer
    {
        public IEnumerator LeftMouseButtonDown(Player player);
        public IEnumerator RightMouseButtonDown(Player player);
    }
}
