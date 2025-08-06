
using UnityEngine;

[CreateAssetMenu(menuName = "State/TradeState")]
public class TradeState : State {
    private bool backButtonPressed;
    private bool tradeFinalised;



    #region State
    public override void enterState() {
        backButtonPressed = false;
        tradeFinalised = false;
        UIPipelineEventHub.Instance.sub_TradeTerminated(backButtonListening);
        UIPipelineEventHub.Instance.sub_TradeLockedIn(tradeFinalisedListening);
    }
    public override bool exitConditionMet() {
        return backButtonPressed || tradeFinalised;
    }
    public override void exitState() {
        UIPipelineEventHub.Instance.unsub_TradeTerminated(backButtonListening);
        UIPipelineEventHub.Instance.unsub_TradeLockedIn(tradeFinalisedListening);
    }
    public override State getNextState() {
        if (tradeFinalised) return allStates.getState<PostTradeState>();
        else if (GameState.game.TurnPlayer.InJail) return allStates.getState<JailPreRollState>();
        else return allStates.getState<PreRollState>();
    }
    #endregion



    #region private
    private void backButtonListening() {
        backButtonPressed = true;
    }
    private void tradeFinalisedListening() {
        tradeFinalised = true;
    }
    #endregion
}
