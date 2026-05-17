using UnityEngine;

namespace UI.Marker
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private GameObject _cylinder;
        [SerializeField] private GameObject _sphere;
        [SerializeField] private GameObject _box;
        private MeshRenderer _mesh;


        public void Init(MarkerType type, float radius)
        {
            _cylinder.SetActive(false);
            _sphere.SetActive(false);
            _box.SetActive(false);
            switch (type)
            {
                case MarkerType.Cylinder:
                    _cylinder.SetActive(true);
                    _mesh = _cylinder.GetComponent<MeshRenderer>();
                    break;
                case MarkerType.Sphere:
                    _sphere.SetActive(true);
                    _mesh = _sphere.GetComponent<MeshRenderer>();
                    break;
                case MarkerType.Box:
                    _box.SetActive(true);
                    _mesh = _box.GetComponent<MeshRenderer>();
                    break;
            }

            transform.localScale = Vector3.one * radius;
        }

        public void SetColor(Color color)
        {
            var propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_Color", color);
            _mesh.SetPropertyBlock(propertyBlock);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }

    public enum MarkerType
    {
        Cylinder,
        Sphere,
        Box,
    }
}
