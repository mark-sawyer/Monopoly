
using UnityEngine;

public static class TestTokenIcon {
    public static TokenSprites tokenTypeToTokenSprites(Token token) {
        switch (token) {
            case Token.BOOT: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/boot_sprites");
            case Token.CAR: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/car_sprites");
            case Token.DOG: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/dog_sprites");
            case Token.HAT: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/hat_sprites");
            case Token.IRON: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/iron_sprites");
            case Token.SHIP: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/ship_sprites");
            case Token.THIMBLE: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/thimble_sprites");
            default: return Resources.Load<TokenSprites>("ScriptableObjects/TokenSprites/wheelbarrow_sprites");
        }
    }
    public static TokenColours playerColourToTokenColours(PlayerColour colour) {
        switch (colour) {
            case PlayerColour.BLUE: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_blue");
            case PlayerColour.GREEN: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_green");
            case PlayerColour.MAGENTA: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_magenta");
            case PlayerColour.ORANGE: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_orange");
            case PlayerColour.PURPLE: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_purple");
            case PlayerColour.RED: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_red");
            case PlayerColour.WHITE: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_white");
            default: return Resources.Load<TokenColours>("ScriptableObjects/TokenColours/token_yellow");
        }
    }
}
