using System;
using UnityEngine;

internal static class EstateColourToColour {
    public static Color exe(EstateColour estateColour) {
        Color getColor(string s) {
            string redString = s.Substring(0, 2);
            string greenString = s.Substring(2, 2);
            string blueString = s.Substring(4, 2);

            float redFloat = Int32.Parse(redString, System.Globalization.NumberStyles.HexNumber);
            float greenFloat = Int32.Parse(greenString, System.Globalization.NumberStyles.HexNumber);
            float blueFloat = Int32.Parse(blueString, System.Globalization.NumberStyles.HexNumber);

            return new Color(redFloat / 255f, greenFloat / 255f, blueFloat / 255f);
        }

        Color colour = Color.black;
        switch(estateColour) {
            case EstateColour.BROWN:
                colour = getColor("590b38");
                break;
            case EstateColour.LIGHT_BLUE:
                colour = getColor("87a5d6");
                break;
            case EstateColour.PINK:
                colour = getColor("ee3878");
                break;
            case EstateColour.ORANGE:
                colour = getColor("f57f23");
                break;
            case EstateColour.RED:
                colour = getColor("ef3923");
                break;
            case EstateColour.YELLOW:
                colour = getColor("fde703");
                break;
            case EstateColour.GREEN:
                colour = getColor("11a55b");
                break;
            case EstateColour.DARK_BLUE:
                colour = getColor("274da1");
                break;
        }
        return colour;
    }
}
