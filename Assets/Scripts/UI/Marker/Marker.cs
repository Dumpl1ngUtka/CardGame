using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public abstract class Marker : MonoBehaviour
    {
        public abstract void Init(SpellObject obj, Vector3 startPos);

        protected virtual void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Render(hit);
            }
        }

        protected abstract void Render(RaycastHit point);
    }
}
