using UnityEngine;

public class PlayerPanel : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    private PlayerInfo player;

    public void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player);
    }
    public void adjustMoney(int difference) {
        moneyAdjuster.adjustMoney(difference);
    }
}
