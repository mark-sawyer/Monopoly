using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class TokenIcon : MonoBehaviour {
    [SerializeField] private TokenSprites tokenSprites;
    [SerializeField] private TokenColours tokenColours;
    [SerializeField] private Image silouhetteImage;
    [SerializeField] private Image tokenImage;
    [SerializeField] private Image outerCircleImage;
    [SerializeField] private Image innerCircleImage;

    public void setup(PlayerInfo player, TokenVisualManager tokenVisualManager) {
        void setAndAdjustImages(RectTransform rt, Image image, Sprite sprite, Color color) {
            image.sprite = sprite;
            float width = image.sprite.rect.width;
            float height = image.sprite.rect.height;
            rt.sizeDelta = new Vector2(width / 10f, height / 10f);
            Vector2 spritePivotNormalized = new Vector2(sprite.pivot.x / width, sprite.pivot.y / height);
            rt.pivot = spritePivotNormalized;
            image.color = color;
        }

        tokenSprites = tokenVisualManager.tokenTypeToTokenSprites(player.Token);
        tokenColours = tokenVisualManager.playerColourToTokenColours(player.Colour);
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
}
