using System.Collections.Generic;
using UnityEngine;

public class TradePanel : MonoBehaviour {
    [SerializeField] private TradePanelSide leftSide;
    [SerializeField] private TradePanelSide rightSide;
    private PlayerInfo leftPlayer;
    private PlayerInfo rightPlayer;



    #region Singleton boilerplate
    public static TradePanel Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
        TradeEventHub.Instance.unsub_TradeChanged(callNewProposedTrade);
        TradeEventHub.Instance.unsub_HandshakeComplete(finaliseTrade);
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        TradeEventHub.Instance.sub_TradeChanged(callNewProposedTrade);
        TradeEventHub.Instance.sub_HandshakeComplete(finaliseTrade);
    }
    #endregion



    #region public
    public void setup(PlayerInfo leftPlayerInfo, PlayerInfo rightPlayerInfo) {
        leftPlayer = leftPlayerInfo;
        rightPlayer = rightPlayerInfo;

        leftSide.setup(leftPlayerInfo);
        rightSide.setup(rightPlayerInfo);
    }
    public ToBeTradedColumn getToBeTradedColumn(PlayerInfo playerInfo) {
        if (playerInfo == leftPlayer) return leftSide.ToBeTradedColumn;
        else return rightSide.ToBeTradedColumn;
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

        SoundPlayer.Instance.play_Flourish();
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
