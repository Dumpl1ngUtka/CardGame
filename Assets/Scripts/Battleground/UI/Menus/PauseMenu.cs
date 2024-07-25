using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public class PauseMenu : UIMenu
    {
        public override void Close()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }
    }
}

