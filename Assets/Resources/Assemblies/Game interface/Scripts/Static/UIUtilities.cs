using System;
using UnityEngine;

public static class UIUtilities {
    public static float scaleForTokens(int tokensOnSpace) {
        switch (tokensOnSpace) {
            case 1:
                return 2.2f;
            case 2:
                return 1.7f;
            case 3:
                return 1.5f;
            case 4:
                return 1.3f;
            case 5:
                return 1.1f;
            case 6:
                return 0.9f;
            case 7:
                return 0.8f;
            default:
                return 0.7f;
        }
    }
    public static float scaleForTokensTwo(SpaceType spaceType, int tokensOnSpace) {
        float estateScale() {
            switch (tokensOnSpace) {
                case 1: return 2.2f;
                case 2: return 1.7f;
                case 3: return 1.5f;
                case 4: return 1.3f;
                case 5: return 1.1f;
                case 6: return 0.9f;
                case 7: return 0.8f;
                default: return 0.7f;
            }
        }
        float otherScale() {
            switch (tokensOnSpace) {
                case 1: return 2.2f;
                case 2: return 1.7f;
                case 3: return 1.5f;
                case 4: return 1.3f;
                case 5: return 1.1f;
                case 6: return 0.9f;
                case 7: return 0.8f;
                default: return 0.7f;
            }
        }
        float cornerScale() {
            switch (tokensOnSpace) {
                case 1: return 2.2f;
                case 2: return 1.7f;
                case 3: return 1.5f;
                case 4: return 1.3f;
                case 5: return 1.1f;
                case 6: return 0.9f;
                case 7: return 0.8f;
                default: return 0.7f;
            }
        }
        float visitingScale() {
            switch (tokensOnSpace) {
                case 1: return 2.2f;
                case 2: return 1.7f;
                case 3: return 1.5f;
                case 4: return 1.3f;
                case 5: return 1.1f;
                case 6: return 0.9f;
                case 7: return 0.8f;
                default: return 0.7f;
            }
        }
        float jailScale() {
            switch (tokensOnSpace) {
                case 1: return 2.2f;
                case 2: return 1.7f;
                case 3: return 1.5f;
                case 4: return 1.3f;
                case 5: return 1.1f;
                case 6: return 0.9f;
                case 7: return 0.8f;
                default: return 0.7f;
            }
        }

        float val = 0;
        switch (spaceType) {
            case SpaceType.ESTATE: val = estateScale(); break;
            case SpaceType.OTHER: val = otherScale(); break;
            case SpaceType.CORNER: val = cornerScale(); break;
            case SpaceType.VISITING: val = visitingScale(); break;
            case SpaceType.JAIL: val = jailScale(); break;
        }
        return val;
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
