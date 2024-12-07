using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground.UI
{
    public static class PlayerColor
    {
        private static Dictionary<int, Color> _colors = new Dictionary<int, Color>()
        {
            { 0, Color.blue },
            { 1, Color.red},
            { 2, Color.yellow},
            { 3, Color.green },
            { 4, new Color(0.5f,0,0.5f) },
            { 5, new Color(1f,0.5f,0) },
            { 6, Color.white },
        };

        public static Color GetColorByID(int id) => _colors.GetValueOrDefault(id);
    }
}

