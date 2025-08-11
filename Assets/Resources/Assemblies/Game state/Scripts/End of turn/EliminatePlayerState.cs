using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "State/EliminatePlayerState")]
internal class EliminatePlayerState : State {
    private bool toUpdateTurnPlayer;
    private bool toAssetAuctioning;
    private PlayerInfo eliminatedPlayer;



    #region State
    public override void enterState() {
        toUpdateTurnPlayer = false;
        toAssetAuctioning = false;

        UIEventHub.Instance.sub_PlayerEliminatedAnimationOver(afterPlayerEliminatedAnimation);
        UIEventHub.Instance.sub_AllExpiredPropertyVisualsUpdated(afterVisualsUpdated);

        eliminatedPlayer = GameState.game.PlayerInDebt;
        DataUIPipelineEventHub.Instance.call_PlayerEliminated(eliminatedPlayer);
    }
    public override bool exitConditionMet() {
        return toUpdateTurnPlayer
            || toAssetAuctioning;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_PlayerEliminatedAnimationOver(afterPlayerEliminatedAnimation);
        UIEventHub.Instance.unsub_AllExpiredPropertyVisualsUpdated(afterVisualsUpdated);
    }
    public override State getNextState() {
        if (toUpdateTurnPlayer) return allStates.getState<UpdateTurnPlayerState>();
        if (toAssetAuctioning) return allStates.getState<AssetAuctioningState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void afterPlayerEliminatedAnimation() {
        DebtInfo debtInfo = GameState.game.BankInfo.EliminatedPlayerDebt;
        IEnumerable<TradableInfo> tradableInfos = GameState.game.BankInfo.EliminatedPlayerAssets;
        if (tradableInfos.Count() == 0) toUpdateTurnPlayer = true;
        else if (debtInfo.Creditor is PlayerInfo creditorPlayer) {
            DataEventHub.Instance.call_TradeCommenced(eliminatedPlayer, creditorPlayer);
            DataUIPipelineEventHub.Instance.call_TradeUpdated(
                tradableInfos.ToList(),
                new List<TradableInfo>(),
                null,
                0
            );
            DataUIPipelineEventHub.Instance.call_TradeLockedIn();
            UIEventHub.Instance.call_UpdateExpiredPropertyVisuals();
        }
        else toAssetAuctioning = true;
    }
    private void afterVisualsUpdated() {
        toUpdateTurnPlayer = true;
    }
    #endregion
}
