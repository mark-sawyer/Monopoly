using UnityEngine;

[CreateAssetMenu(menuName = "Colour/CardColourDictionary")]
public class CardColourDictionary : ScriptableObject {
    [SerializeField] private GameColour chanceColour;
    [SerializeField] private GameColour communityChestColour;
    [SerializeField] private GameColour chancePadlockColour;
    [SerializeField] private GameColour communityChestPadlockColour;
    private static CardColourDictionary instance;



    #region public
    public static CardColourDictionary Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<CardColourDictionary>(
                    "ScriptableObjects/Colours/card_colour_dictionary"
                );
            }
            return instance;
        }
    }
    public Color lookupCardColour(CardType cardType) {
        if (cardType == CardType.CHANCE) return chanceColour.Colour;
        else return communityChestColour.Colour;
    }
    public Color lookupPadlockColour(CardType cardType) {
        if (cardType == CardType.CHANCE) return chancePadlockColour.Colour;
        else return communityChestPadlockColour.Colour;
    }
    #endregion
}
