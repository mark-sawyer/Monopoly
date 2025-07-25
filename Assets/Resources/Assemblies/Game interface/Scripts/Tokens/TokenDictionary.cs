using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TokenDictionary", menuName = "Token/TokenDictionary")]
public class TokenDictionary : ScriptableObject {
    [SerializeField] private TokenColours[] tokenColours;
    [SerializeField] private TokenSprites[] tokenSprites;
    #region TokenColours properties
    private TokenColours Blue => tokenColours[0];
    private TokenColours Green => tokenColours[1];
    private TokenColours Magenta => tokenColours[2];
    private TokenColours Orange => tokenColours[3];
    private TokenColours Purple => tokenColours[4];
    private TokenColours Red => tokenColours[5];
    private TokenColours White => tokenColours[6];
    private TokenColours Yellow => tokenColours[7];
    #endregion
    #region TokenSprites properties
    private TokenSprites Boot => tokenSprites[0];
    private TokenSprites Car => tokenSprites[1];
    private TokenSprites Dog => tokenSprites[2];
    private TokenSprites Hat => tokenSprites[3];
    private TokenSprites Iron => tokenSprites[4];
    private TokenSprites Ship => tokenSprites[5];
    private TokenSprites Thimble => tokenSprites[6];
    private TokenSprites Wheelbarrow => tokenSprites[7];
    #endregion
    private Dictionary<PlayerColour, TokenColours> coloursDict;
    private Dictionary<Token, TokenSprites> spritesDict;
    private static TokenDictionary instance;



    #region public
    public static TokenDictionary Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<TokenDictionary>(
                    "ScriptableObjects/Tokens/token_dictionary"
                );
            }
            return instance;
        }
    }
    public TokenColours getColours(PlayerColour colour) {
        if (coloursDict == null) coloursDict = initialiseColoursDict();
        return coloursDict[colour];
    }
    public TokenSprites getSprites(Token token) {
        if (spritesDict == null) spritesDict = initialiseSpritesDict();
        return spritesDict[token];
    }
    #endregion



    #region initialise
    private Dictionary<PlayerColour, TokenColours> initialiseColoursDict() {
        Dictionary<PlayerColour, TokenColours> newDict = new();
        newDict[PlayerColour.BLUE] = Blue;
        newDict[PlayerColour.GREEN] = Green;
        newDict[PlayerColour.MAGENTA] = Magenta;
        newDict[PlayerColour.ORANGE] = Orange;
        newDict[PlayerColour.PURPLE] = Purple;
        newDict[PlayerColour.RED] = Red;
        newDict[PlayerColour.WHITE] = White;
        newDict[PlayerColour.YELLOW] = Yellow;
        return newDict;
    }
    private Dictionary<Token, TokenSprites> initialiseSpritesDict() {
        Dictionary<Token, TokenSprites> newDict = new();
        newDict[Token.BOOT] = Boot;
        newDict[Token.CAR] = Car;
        newDict[Token.DOG] = Dog;
        newDict[Token.HAT] = Hat;
        newDict[Token.IRON] = Iron;
        newDict[Token.SHIP] = Ship;
        newDict[Token.THIMBLE] = Thimble;
        newDict[Token.WHEELBARROW] = Wheelbarrow;
        return newDict;
    }
    #endregion
}
