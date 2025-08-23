using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "State/EliminatePlayerState")]
internal class EliminatePlayerState : State {
    private bool toResolveDebt;
    private bool toAssetAuctioning;
    private bool gameComplete;
    private PlayerInfo eliminatedPlayer;



    #region State
    public override void enterState() {
        toResolveDebt = false;
        toAssetAuctioning = false;
        gameComplete = false;
        UIEventHub.Instance.sub_AllExpiredPropertyVisualsUpdated(uiUpdatesPreEliminationOver);
        UIEventHub.Instance.sub_PlayerEliminatedAnimationOver(afterPlayerEliminatedAnimation);



        PlayerInfo[] playersNeedingMoneyUIUpdate = PlayerPanelManager.Instance.getPlayersNeedingMoneyUIUpdate();
        if (playersNeedingMoneyUIUpdate.Length > 0) {
            UIEventHub.Instance.call_UpdateUIMoney(playersNeedingMoneyUIUpdate);
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE,
                () => UIEventHub.Instance.call_UpdateExpiredPropertyVisuals()
            );
        }
        else {
            UIEventHub.Instance.call_UpdateExpiredPropertyVisuals();
        }
    }
    public override bool exitConditionMet() {
        return toResolveDebt
            || toAssetAuctioning
            || gameComplete;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_PlayerEliminatedAnimationOver(afterPlayerEliminatedAnimation);
        UIEventHub.Instance.unsub_AllExpiredPropertyVisualsUpdated(afterAssetRedistribution);
    }
    public override State getNextState() {
        if (toResolveDebt) return allStates.getState<ResolveDebtState>();
        if (toAssetAuctioning) return allStates.getState<AssetAuctioningState>();
        if (gameComplete) return allStates.getState<GameCompleteState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void uiUpdatesPreEliminationOver() {
        WaitFrames.Instance.beforeAction(
            50,
            () => {
                eliminatedPlayer = GameState.game.PlayerInDebt;
                DataUIPipelineEventHub.Instance.call_PlayerEliminated(eliminatedPlayer);
                UIEventHub.Instance.unsub_AllExpiredPropertyVisualsUpdated(uiUpdatesPreEliminationOver);
                UIEventHub.Instance.sub_AllExpiredPropertyVisualsUpdated(afterAssetRedistribution);
            }
        );
    }
    private void afterPlayerEliminatedAnimation() {
        DebtInfo debtInfo = GameState.game.BankInfo.EliminatedPlayerDebt;
        IEnumerable<TradableInfo> eliminatedPlayerAssets = GameState.game.BankInfo.EliminatedPlayerAssets;


        if (GameState.game.ActivePlayers.Count() == 1) gameComplete = true;
        else if (eliminatedPlayerAssets.Count() == 0) toResolveDebt = true;
        else if (debtInfo is SingleCreditorDebtInfo singleCreditorDebtInfo && singleCreditorDebtInfo.Creditor is PlayerInfo creditorPlayer) {
            DataEventHub.Instance.call_TradeCommenced(eliminatedPlayer, creditorPlayer);
            DataUIPipelineEventHub.Instance.call_TradeUpdated(
                eliminatedPlayerAssets.ToList(),
                new List<TradableInfo>(),
                null,
                0
            );
            DataUIPipelineEventHub.Instance.call_TradeLockedIn();
            UIEventHub.Instance.call_UpdateExpiredPropertyVisuals();
        }
        else {
            List<CardInfo> cardInfos = eliminatedPlayerAssets.OfType<CardInfo>().ToList();
            foreach (CardInfo cardInfo in cardInfos) {
                DataEventHub.Instance.call_CardReturned(cardInfo);
            }
            int properties = eliminatedPlayerAssets.Count(x => x is PropertyInfo);
            if (properties > 0) toAssetAuctioning = true;
            else toResolveDebt = true;
        }
    }
    private void afterAssetRedistribution() {
        toResolveDebt = true;
    }
    #endregion
}
