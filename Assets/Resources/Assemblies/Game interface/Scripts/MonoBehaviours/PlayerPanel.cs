using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private Image image;
    private Player player;

    public void setup(Player player) {
        this.player = player;
        image.sprite = UIUtilities.tokenTypeToSprite(player.getToken());
    }
}
