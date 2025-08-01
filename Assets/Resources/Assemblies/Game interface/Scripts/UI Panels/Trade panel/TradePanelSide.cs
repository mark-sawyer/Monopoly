using System.Collections.Generic;
using UnityEngine;

public class TradePanelSide : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private OwnedTradables ownedTradables;



    public void setup(PlayerInfo playerInfo) {
        Token token = playerInfo.Token;
        PlayerColour colour = playerInfo.Colour;
        int playerMoney = playerInfo.Money;
        IEnumerable<TradableInfo> tradableInfos = playerInfo.TradableInfos;

        tokenIcon.setup(token, colour);
        moneyAdjuster.setStartingMoney(playerMoney);
        ownedTradables.setup(tradableInfos);
    }
}
