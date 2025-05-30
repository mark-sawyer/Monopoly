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
        setAndAdjustImages(
            (RectTransform)silouhetteImage.transform,
            silouhetteImage,
            tokenSprites.SilouhetteSprite,
            tokenColours.OutlineColour
        );
        setAndAdjustImages(
            (RectTransform)tokenImage.transform,
            tokenImage,
            tokenSprites.ForegroundSprite,
            tokenColours.TokenColour
        );
        outerCircleImage.color = tokenColours.OuterCircleColour;
        innerCircleImage.color = tokenColours.InnerCircleColour;
    }
    private void setAndAdjustImages(RectTransform rt, Image image, Sprite sprite, Color color) {
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);

        image.sprite = sprite;
        float width = image.sprite.rect.width;
        float height = image.sprite.rect.height;

        Vector2 spritePivotNormalized = new Vector2(sprite.pivot.x / width, sprite.pivot.y / height);
        rt.pivot = spritePivotNormalized;
        rt.sizeDelta = new Vector2(width / 10f, height / 10f);
        image.color = color;
        reanchor(rt, (RectTransform)rt.parent);
    }
    private void reanchor(RectTransform rt, RectTransform parent) {
        Vector2 parentSize = parent.rect.size;
        Vector2 pos = rt.anchoredPosition;
        Vector2 size = rt.sizeDelta;

        Vector2 lowerLeft = pos - rt.pivot * size;
        Vector2 upperRight = pos + (Vector2.one - rt.pivot) * size;
        Vector2 parentLowerLeft = pos - rt.anchorMin * parentSize;
        Vector2 anchorMin = (lowerLeft - parentLowerLeft) / parentSize;
        Vector2 anchorMax = (upperRight - parentLowerLeft) / parentSize;

        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
