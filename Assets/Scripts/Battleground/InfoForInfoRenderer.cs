using Battleground;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public struct InfoForInfoRenderer
    {
        public string Title;
        public string UnderTitle;
        public string ContentLine1;
        public string ContentLine2;
        public string ContentLine3;
        public float HealthBarFill;
        public float StaminaBarFill;
        public IObjectForInfoRenderer[] ObjectsForCardRenderers;
    }
}
