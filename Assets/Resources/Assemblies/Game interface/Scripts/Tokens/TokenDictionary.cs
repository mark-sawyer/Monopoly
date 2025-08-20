using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TokenDictionary", menuName = "Token/TokenDictionary")]
public class TokenDictionary : ScriptableObject {
    [SerializeField] private TokenColours[] tokenColours;
    [SerializeField] private TokenSprites[] tokenSprites;
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
        for (int i = 0; i < 8; i++) {
            newDict[(PlayerColour)i] = tokenColours[i];
        }
        return newDict;
    }
    private Dictionary<Token, TokenSprites> initialiseSpritesDict() {
        Dictionary<Token, TokenSprites> newDict = new();
        for (int i = 0; i < 8; i++) {
            newDict[(Token)i] = tokenSprites[i];
        }
        return newDict;
    }
    #endregion
}
