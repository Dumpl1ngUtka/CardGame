using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public abstract class UIMenu : MonoBehaviour
    {
        public abstract void Open();

        public abstract void Close();
    }
}