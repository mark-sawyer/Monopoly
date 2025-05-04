using System;
using UnityEngine;

public static class UIUtilities {
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
                colour = getColor("945232");
                break;
            case EstateColour.LIGHT_BLUE:
                colour = getColor("aee0fb");
                break;
            case EstateColour.PINK:
                colour = getColor("df3c97");
                break;
            case EstateColour.ORANGE:
                colour = getColor("f89418");
                break;
            case EstateColour.RED:
                colour = getColor("f61d26");
                break;
            case EstateColour.YELLOW:
                colour = getColor("fef907");
                break;
            case EstateColour.GREEN:
                colour = getColor("27c066");
                break;
            case EstateColour.DARK_BLUE:
                colour = getColor("0171bb");
                break;
        }
        return colour;
    }
    public static Sprite tokenTypeToSpriteForeground(Token tokenType) {
        Sprite sprite = null;
        switch (tokenType) {
            case Token.BOOT:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/boot_foreground");
                break;
            case Token.CAR:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/car_foreground");
                break;
            case Token.DOG:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/dog_foreground");
                break;
            case Token.HAT:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/hat_foreground");
                break;
            case Token.IRON:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/iron_foreground");
                break;
            case Token.SHIP:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/ship_foreground");
                break;
            case Token.THIMBLE:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/thimble_foreground");
                break;
            case Token.WHEELBARROW:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Pieces/wheelbarrow_foreground");
                break;
        }
        return sprite;
    }
    public static Sprite tokenTypeToSpriteBackground(Token tokenType) {
        Sprite sprite = null;
        switch (tokenType) {
            case Token.BOOT:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/boot_background");
                break;
            case Token.CAR:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/car_background");
                break;
            case Token.DOG:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/dog_background");
                break;
            case Token.HAT:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/hat_background");
                break;
            case Token.IRON:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/iron_background");
                break;
            case Token.SHIP:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/ship_background");
                break;
            case Token.THIMBLE:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/thimble_background");
                break;
            case Token.WHEELBARROW:
                sprite = Resources.Load<Sprite>("Sprites/Tokens/Silouhettes/wheelbarrow_background");
                break;
        }
        return sprite;
    }
}
