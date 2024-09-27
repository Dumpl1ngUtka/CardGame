using Battleground;
using UnityEngine;

namespace UI.Marker
{
    public abstract class Marker : MonoBehaviour
    {
        protected abstract LayerMask Mask { get; }

        protected virtual void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, Mask))
            {
                Render(hit);
            }
        }

        protected abstract void Render(RaycastHit point);
    }
}
