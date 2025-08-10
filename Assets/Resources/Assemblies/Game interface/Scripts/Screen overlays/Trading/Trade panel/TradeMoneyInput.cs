using UnityEngine;

public class TradeMoneyInput : MoneyInput {
    [SerializeField] private MoneyInput otherMoneyInput;
    [SerializeField] private bool isLeftPlayer;
    private TradeEventHub tradeEventHub;



    #region public
    public override void setup(PlayerInfo playerInfo) {
        base.setup(playerInfo);
        tradeEventHub = TradeEventHub.Instance;
    }
    #endregion



    #region protected
    protected override void postValueEnteredReaction(int bid) {
        otherMoneyInput.clearInput();
        tradeEventHub.call_TradeChanged();
    }
    #endregion
}
