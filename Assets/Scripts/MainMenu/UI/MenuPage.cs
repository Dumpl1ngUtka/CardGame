using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.UI
{
    public class MenuPage : MonoBehaviour
    {
        [SerializeField] private MenuTab[] _tabs;

        private const float _tabChageAnimationTime = 0.1f;

        public IEnumerator Open()
        {
            gameObject.SetActive(true);
            foreach (var tab in _tabs)
                tab.SetRotation(90);
            yield return new WaitForSeconds(_tabChageAnimationTime);
            var timer = 0f;
            while (timer < _tabChageAnimationTime)
            {
                timer += Time.deltaTime;
                var rotation = Mathf.Lerp(90, 0, timer / _tabChageAnimationTime);
                foreach (var tab in _tabs)
                    tab.SetRotation(rotation);
                yield return null;
            }
        }

        public IEnumerator Close()
        {
            var timer = 0f;
            while (timer < _tabChageAnimationTime)
            {
                timer += Time.deltaTime;
                var rotation = Mathf.Lerp(0, 90, timer / _tabChageAnimationTime);
                foreach (var tab in _tabs)
                    tab.SetRotation(rotation);
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
