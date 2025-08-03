using System.Collections.Generic;
using UnityEngine;

public class TradePanel : MonoBehaviour {
    [SerializeField] private TradePanelSide leftSide;
    [SerializeField] private TradePanelSide rightSide;
    private PlayerInfo leftPlayer;
    private PlayerInfo rightPlayer;
    private TradeEventHub tradeEventHub;
    private DataEventHub dataEventHub;



    #region MonoBehaviour
    private void Start() {
        dataEventHub = DataEventHub.Instance;
        tradeEventHub = TradeEventHub.Instance;
        tradeEventHub.sub_TradeChanged(callNewProposedTrade);
    }
    private void OnDestroy() {
        tradeEventHub.unsub_TradeChanged(callNewProposedTrade);
    }
    #endregion



    #region public
    public void setup(PlayerInfo leftPlayerInfo, PlayerInfo rightPlayerInfo) {
        leftPlayer = leftPlayerInfo;
        rightPlayer = rightPlayerInfo;

        leftSide.setup(leftPlayerInfo);
        rightSide.setup(rightPlayerInfo);
    }
    #endregion



    #region private
    private void callNewProposedTrade() {
        List<TradableInfo> tradablesOne = leftSide.getProposedTradables();
        List<TradableInfo> tradablesTwo = rightSide.getProposedTradables();
        int leftMoney = leftSide.inputMoney();
        int rightMoney = rightSide.inputMoney();
        PlayerInfo moneyGivingPlayer = null;
        int givenMoney = 0;
        if (leftMoney > 0 && rightMoney > 0) throw new System.Exception("Both players cannot trade money.");
        else if (leftMoney > 0) {
            moneyGivingPlayer = leftPlayer;
            givenMoney = leftMoney;
        }
        else if (rightMoney > 0) {
            moneyGivingPlayer = rightPlayer;
            givenMoney = rightMoney;
        }

        dataEventHub.call_TradeUpdated(
            tradablesOne,
            tradablesTwo,
            moneyGivingPlayer,
            givenMoney
        );
    }
    #endregion
}
