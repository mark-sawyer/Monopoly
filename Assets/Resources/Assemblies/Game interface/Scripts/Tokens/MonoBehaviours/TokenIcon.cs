using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TokenIcon : MonoBehaviour {
    [SerializeField] private TokenDictionary tokenDictionary;
    [SerializeField] private Image silouhetteImage;
    [SerializeField] private Image tokenImage;
    [SerializeField] private Image outerCircleImage;
    [SerializeField] private Image innerCircleImage;
    private Token token;
    private PlayerColour colour;



    public Token Token => token;
    public PlayerColour Colour => colour;
    public void setup(Token token, PlayerColour colour) {
        this.token = token;
        this.colour = colour;
        TokenSprites tokenSprites = tokenDictionary.getSprites(token);
        TokenColours tokenColours = tokenDictionary.getColours(colour);

        silouhetteImage.sprite = tokenSprites.SilouhetteSprite;
        silouhetteImage.color = tokenColours.OutlineColour;

        tokenImage.sprite = tokenSprites.ForegroundSprite;
        tokenImage.color = tokenColours.TokenColour;

        outerCircleImage.color = tokenColours.OuterCircleColour;
        innerCircleImage.color = tokenColours.InnerCircleColour;
    }
}
