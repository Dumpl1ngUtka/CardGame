using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public static class RareColors
    {
        private static readonly Color[] _colors = new Color[6]
        {
        new Color(0,0,0,1),
        new Color(1, 1, 1, 1),
        new Color(0, 1, 0, 1),
        new Color(0, 0, 1,1),
        new Color(1, 0, 1, 1),
        new Color(1, 0.9f, 0, 1),
        };

        public static Color GetColorByStarCount(int starCount)
        {
            return _colors[starCount];
        }
    }
}

