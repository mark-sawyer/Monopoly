
using UnityEngine;

[CreateAssetMenu(menuName = "State/TradeState")]
public class TradeState : State {
    private bool backButtonPressed;



    #region State
    public override void enterState() {
        backButtonPressed = false;
        UIEventHub.Instance.sub_TradeBackClicked(backButtonListening);
    }
    public override bool exitConditionMet() {
        return backButtonPressed;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TradeBackClicked(backButtonListening);
        UIEventHub.Instance.call_TurnMenuClosed();
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
    private void backButtonListening() {
        backButtonPressed = true;
    }
    #endregion
}
