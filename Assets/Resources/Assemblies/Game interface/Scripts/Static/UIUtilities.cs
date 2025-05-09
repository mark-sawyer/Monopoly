using System;
using UnityEngine;

public static class UIUtilities {
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
