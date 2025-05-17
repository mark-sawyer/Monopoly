using UnityEngine;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] TokenIcon tokenIcon;
    private PlayerInfo player;

    public void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player);
    }
}
