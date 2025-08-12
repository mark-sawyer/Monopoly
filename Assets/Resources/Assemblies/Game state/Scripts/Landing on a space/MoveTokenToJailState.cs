using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenToJail")]
internal class MoveTokenToJailState : State {
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        tokenSettled = false;

        ScreenOverlayEventHub.Instance.sub_RemoveScreenOverlay(animationOverCalled);
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);

        DataEventHub.Instance.call_TurnPlayerWillLoseTurn();
        ScreenOverlayEventHub.Instance.call_SpinningPoliceman();
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenOverlay(animationOverCalled);
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
    #endregion



    #region private
    private void animationOverCalled() {
        int startingIndex = GameState.game.TurnPlayer.SpaceIndex;
        DataUIPipelineEventHub.Instance.call_TurnPlayerSentToJail(startingIndex);
    }
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
