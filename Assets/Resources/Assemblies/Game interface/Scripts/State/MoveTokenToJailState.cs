using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenToJail")]
public class MoveTokenToJailState : State {
    private bool tokenSettled;


    #region GameState
    public override void enterState() {
        tokenSettled = false;

        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);

        ScreenAnimationEventHub.Instance.call_SpinningPoliceman();
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    #region private
    private void animationOverCalled() {
        int startingIndex = GameState.game.SpaceIndexOfTurnPlayer;
        DataEventHub.Instance.call_TurnPlayerSentToJail(startingIndex);
    }
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
