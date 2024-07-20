using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(menuName = "Spells/TestSpell")]
    public class TestSpell : Spell
    {
        public override void LeftMouseClick(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }

        public override void RightMouseClick(RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            var timer = 0f;
            while (timer < 10)
            {
                Debug.Log(timer);
                timer += Time.deltaTime;
            }
        }
    }

}