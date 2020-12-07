using UnityEngine;
using System.Collections;
using System;

namespace Assets.scripts.Utils
{
    public class ColorUtils
    {
        public static KeyPickup.KeyColor[] ColorList { get; }

        static ColorUtils()
        {
            ColorList = (KeyPickup.KeyColor[])Enum.GetValues(typeof(KeyPickup.KeyColor));
        }
        public static int getKey(KeyPickup.KeyColor color)
        {
            switch (color)
            {
                case KeyPickup.KeyColor.RED:
                    return 0;
                case KeyPickup.KeyColor.BLUE:
                    return 1;
                case KeyPickup.KeyColor.YELLOW:
                    return 2;
                case KeyPickup.KeyColor.PURPLE:
                    return 3;
                case KeyPickup.KeyColor.NONE:
                    return 4;
            }
            return -1;
        }
        public static String getColorName(KeyPickup.KeyColor color)
        {
            switch (color)
            {
                case KeyPickup.KeyColor.RED:
                    return "red";
                case KeyPickup.KeyColor.BLUE:
                    return "blue";
                case KeyPickup.KeyColor.YELLOW:
                    return "yellow";
                case KeyPickup.KeyColor.PURPLE:
                    return "purple";
                case KeyPickup.KeyColor.NONE:
                    return "";
            }
            return "this shuoldn't happen";
        }
    }

}