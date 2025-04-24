using System;
using UnityEngine;

public static class UIUtilities {
    public static Vector3 spaceIndexToPosition(int index) {
        string stringIndex = index.ToString().PadLeft(2, '0');
        GameObject spaceGameObject = GameObject.Find("space_" + stringIndex);
        Vector3 localOffset = new Vector3(3.7f, -6f, -2f);
        return spaceGameObject.transform.TransformPoint(localOffset);
    }

    public static Color estateColourToColour(EstateColour estateColour) {
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
        switch (estateColour) {
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

    public static Sprite tokenTypeToSprite(Token tokenType) {
        Sprite sprite = null;
        switch (tokenType) {
            case Token.BOOT:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/boot");
                break;
            case Token.CAR:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/car");
                break;
            case Token.DOG:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/dog");
                break;
            case Token.HAT:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/hat");
                break;
            case Token.IRON:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/iron");
                break;
            case Token.SHIP:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/ship");
                break;
            case Token.THIMBLE:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/thimble");
                break;
            case Token.WHEELBARROW:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/wheelbarrow");
                break;
        }
        return sprite;
    }
}
