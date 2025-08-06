using UnityEngine;

[CreateAssetMenu(menuName = "State/PostTradeState")]
public class PostTradeState : State {
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
        if (GameState.game.TurnPlayer.InJail) {
            return allStates.getState<JailPreRollState>();
        }
        else {
            return allStates.getState<PreRollState>();
        }
    }
    #endregion



    #region private
    private void updateAnimationsOverListener() {
        updateAnimationsOver = true;
    }
    #endregion
}
