
using UnityEngine;

[CreateAssetMenu(menuName = "State/TradeState")]
internal class TradeState : State {
    private bool backButtonPressed;
    private bool tradeFinalised;



    #region State
    public override void enterState() {
        backButtonPressed = false;
        tradeFinalised = false;
        UIPipelineEventHub.Instance.sub_TradeTerminated(backButtonListening);
        UIPipelineEventHub.Instance.sub_TradeLockedIn(tradeFinalisedListening);

        SoundOnlyEventHub.Instance.call_OtherChime();
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
        else return allStates.getState<PrerollState>();
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
