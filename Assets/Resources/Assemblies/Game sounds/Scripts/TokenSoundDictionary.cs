using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TokenDictionary", menuName = "Token/TokenSoundDictionary")]
public class TokenSoundDictionary : ScriptableObject {
    [SerializeField] private AudioClip[] tokenSounds;
    [SerializeField] private AudioClip[] colourSounds;
    private Dictionary<PlayerColour, AudioClip> colourSoundsDict;
    private Dictionary<Token, AudioClip> tokenSoundsDict;
    private static TokenSoundDictionary instance;



    #region public
    public static TokenSoundDictionary Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<TokenSoundDictionary>(
                    "ScriptableObjects/Tokens/token_sound_dictionary"
                );
            }
            return instance;
        }
    }
    public AudioClip getColourSound(PlayerColour colour) {
        if (colourSoundsDict == null) colourSoundsDict = initialiseColourSoundsDict();
        return colourSoundsDict[colour];
    }
    public AudioClip getTokenSound(Token token) {
        if (tokenSoundsDict == null) tokenSoundsDict = initialiseTokenSoundsDict();
        return tokenSoundsDict[token];
    }
    #endregion




    #region initialise
    private Dictionary<PlayerColour, AudioClip> initialiseColourSoundsDict() {
        Dictionary<PlayerColour, AudioClip> newDict = new();
        for (int i = 0; i < 8; i++) {
            newDict[(PlayerColour)i] = colourSounds[i];
        }
        return newDict;
    }
    private Dictionary<Token, AudioClip> initialiseTokenSoundsDict() {
        Dictionary<Token, AudioClip> newDict = new();
        for (int i = 0; i < 8; i++) {
            newDict[(Token)i] = tokenSounds[i];
        }
        return newDict;
    }
    #endregion
}
