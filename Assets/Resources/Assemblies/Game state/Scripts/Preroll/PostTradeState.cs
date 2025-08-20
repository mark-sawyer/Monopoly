using UnityEngine;

[CreateAssetMenu(menuName = "State/PostTradeState")]
internal class PostTradeState : State {
    private bool updateAnimationsOver;



    #region State
    public override void enterState() {
        updateAnimationsOver = false;
        TradeEventHub.Instance.sub_AllVisualsUpdatedAfterTradeFinalised(updateAnimationsOverListener);
        TradeEventHub.Instance.call_UpdateVisualsAfterTradeFinalised();
    }
    public override bool exitConditionMet() {
        return updateAnimationsOver;
    }
    public override void exitState() {
        TradeEventHub.Instance.unsub_AllVisualsUpdatedAfterTradeFinalised(updateAnimationsOverListener);
    }
    public override State getNextState() {
        return allStates.getState<ResolveMortgageState>();
    }
    #endregion



    #region private
    private void updateAnimationsOverListener() {
        updateAnimationsOver = true;
    }
    #endregion
}
