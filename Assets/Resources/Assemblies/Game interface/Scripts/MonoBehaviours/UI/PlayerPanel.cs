using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private Image pieceImage;
    [SerializeField] private Image silouhetteImage;
    private PlayerInfo player;

    public void setup(PlayerInfo player, TokenVisualManager tokenVisualManager) {
        this.player = player;
        TokenSprites tokenSprites = tokenVisualManager.tokenTypeToTokenSprites(player.Token);


        Sprite silouhetteSprite = tokenSprites.SilouhetteSprite;
        silouhetteImage.sprite = silouhetteSprite;
        resizeTokenObject(silouhetteSprite, 2, "silouhette");
        
        Sprite tokenSprite = tokenSprites.ForegroundSprite;
        pieceImage.sprite = tokenSprite;
        resizeTokenObject(tokenSprite, 3, "token");
    }

    private void resizeTokenObject(Sprite tokenSprite, int childIndex, string gameObjectName) {
        float width = tokenSprite.rect.width;
        float height = tokenSprite.rect.height;
        RectTransform tokenTransform = (RectTransform)transform.GetChild(childIndex);
        if (tokenTransform.name != gameObjectName) throw new Exception("Incorrect child referenced.");
        tokenTransform.sizeDelta = InterfaceConstants.PANEL_TOKEN_SIZE_SCALAR * new Vector2(width, height);
    }
}
