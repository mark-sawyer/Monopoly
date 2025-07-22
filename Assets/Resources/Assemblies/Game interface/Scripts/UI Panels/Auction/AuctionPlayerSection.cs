using UnityEngine;

public class AuctionPlayerSection : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private BidInput bidInput;



    public void setup(PlayerInfo playerInfo) {
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
        moneyAdjuster.adjustMoneyQuietly(playerInfo);
        bidInput.setup(playerInfo.Money);
    }
}
