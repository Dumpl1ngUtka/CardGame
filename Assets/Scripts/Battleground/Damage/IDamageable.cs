using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public interface IDamageable 
    {
        public void ApplyDamage(Damage damage);
    }
}

