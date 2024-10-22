using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Battleground.UI
{
    public class TimelineColorIndicator : MonoBehaviour
    {
        //[SerializeField] private Timeline _timeline;
        //[SerializeField] private Transform _container;
        //[SerializeField] private TimelineCell _cellPrefab;
        //private RectTransform _rectTransform;
        //private float _width;
        //private float _stepSizeByPixels => _width/ _timeline.TimeRange;

        //private void OnEnable()
        //{
        //    _rectTransform = GetComponent<RectTransform>();
        //    _width = _rectTransform.rect.size.x;
        //}

        //public void Render(List<Spell> spells)
        //{
        //    ClearContainer();
        //    foreach (Spell spell in spells)
        //    {
        //        var inst = Instantiate(_cellPrefab, _container);
        //        inst.GetComponent<RectTransform>().offsetMin = new ((spell.StartTime - _timeline.MinTime) * _stepSizeByPixels - _width / 2, 0);
        //        inst.GetComponent<RectTransform>().offsetMax = new ((spell.EndTime - _timeline.MinTime) * _stepSizeByPixels - _width / 2, 0);
        //        inst.Init(spell);
        //    }
                
        //}

        //private void ClearContainer()
        //{
        //    foreach (Transform child in _container)
        //    {
        //        Destroy(child.gameObject);
        //    }
        //}
    }
}
