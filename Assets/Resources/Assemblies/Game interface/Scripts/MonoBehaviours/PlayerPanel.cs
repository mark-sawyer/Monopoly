using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private Image image;
    private Player player;

    public void setup(Player player) {
        this.player = player;
        Sprite tokenSprite = UIUtilities.tokenTypeToSprite(player.getToken());
        image.sprite = tokenSprite;
        resizeTokenObject(tokenSprite);
    }

    private void resizeTokenObject(Sprite tokenSprite) {
        float width = tokenSprite.rect.width;
        float height = tokenSprite.rect.height;
        RectTransform tokenTransform = (RectTransform)transform.GetChild(2);
        if (tokenTransform.name != "token") throw new Exception("Incorrect child referenced.");
        tokenTransform.sizeDelta = InterfaceConstants.PANEL_TOKEN_SIZE_SCALAR * new Vector2(width, height);
    }
}
