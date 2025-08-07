using UnityEngine;

[CreateAssetMenu(menuName = "State/IncomeTaxState")]
internal class IncomeTaxState : State {
    private bool questionAnswered;



    #region GameState
    public override void enterState() {
        questionAnswered = false;
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(screenAnimationRemoved);

        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        ScreenAnimationEventHub.Instance.call_IncomeTaxQuestion(playerInfo);
    }
    public override bool exitConditionMet() {
        return questionAnswered;
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(screenAnimationRemoved);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    private void screenAnimationRemoved() {
        questionAnswered = true;
    }
}
