using Battleground;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _currentPosition;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
            }
        }
    }

    private void OnMouseDrag()
    {
        //var dragableItem = hit.transform.GetComponent<IDragable>();
    }
}
