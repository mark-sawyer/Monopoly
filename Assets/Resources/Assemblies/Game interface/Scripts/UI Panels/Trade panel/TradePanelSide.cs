using System.Collections.Generic;
using UnityEngine;

public class TradePanelSide : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private OwnedTradables ownedTradables;
    [SerializeField] private ToBeTradedColumn toBeTradedColumn;



    #region public
    public void setup(PlayerInfo playerInfo) {
        Token token = playerInfo.Token;
        PlayerColour colour = playerInfo.Colour;
        int playerMoney = playerInfo.Money;

        tokenIcon.setup(token, colour);
        moneyAdjuster.setStartingMoney(playerMoney);
        ownedTradables.setup(playerInfo);
        toBeTradedColumn.setup(playerInfo);
    }
    public List<TradableInfo> getProposedTradables() {
        return toBeTradedColumn.getProposedTradables();
    }
    public int inputMoney() {
        return toBeTradedColumn.inputMoney();
    }
    #endregion
}
