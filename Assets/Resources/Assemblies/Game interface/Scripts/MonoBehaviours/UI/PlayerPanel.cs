using UnityEngine;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] TokenIcon tokenIcon;
    private PlayerInfo player;

    public void setup(PlayerInfo player, TokenVisualManager tokenVisualManager) {
        this.player = player;
        TokenSprites tokenSprites = tokenVisualManager.tokenTypeToTokenSprites(player.Token);
        tokenIcon.setup(player, tokenVisualManager);
    }
}
