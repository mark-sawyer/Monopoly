using System.Collections.Generic;
using UnityEngine;

public class TradePanel : MonoBehaviour {
    [SerializeField] private TradePanelSide leftSide;
    [SerializeField] private TradePanelSide rightSide;
    private PlayerInfo leftPlayer;
    private PlayerInfo rightPlayer;



    #region MonoBehaviour
    private void Start() {
        TradeEventHub.Instance.sub_TradeChanged(callNewProposedTrade);
        TradeEventHub.Instance.sub_HandshakeComplete(finaliseTrade);
    }
    private void OnDestroy() {
        TradeEventHub.Instance.unsub_TradeChanged(callNewProposedTrade);
        TradeEventHub.Instance.unsub_HandshakeComplete(finaliseTrade);
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
    private bool TradeConditionsMet {
        get {
            return !GameState.game.TradeIsEmpty
                && leftSide.AgreeCompressed
                && rightSide.AgreeCompressed;
        }
    }
    private void finaliseTrade() {
        if (!TradeConditionsMet) return;  // Defensive check in case the proposed trade was adjusted on the last frame.

        SoundOnlyEventHub.Instance.call_Flourish();
        DataUIPipelineEventHub.Instance.call_TradeLockedIn();
    }
    private void callNewProposedTrade() {
        List<TradableInfo> tradablesOne = leftSide.ProposedTradables;
        List<TradableInfo> tradablesTwo = rightSide.ProposedTradables;
        int leftMoney = leftSide.InputMoney;
        int rightMoney = rightSide.InputMoney;
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

        DataUIPipelineEventHub.Instance.call_TradeUpdated(
            tradablesOne,
            tradablesTwo,
            moneyGivingPlayer,
            givenMoney
        );
    }
    #endregion
}
