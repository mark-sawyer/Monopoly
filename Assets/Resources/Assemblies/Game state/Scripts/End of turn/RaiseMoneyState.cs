using UnityEngine;

[CreateAssetMenu(menuName = "State/RaiseMoneyState")]
internal class RaiseMoneyState : State {
    private bool panelRemoved;



    #region State
    public override void enterState() {
        panelRemoved = false;
        DebtInfo debtInfo = GameState.game.TurnPlayer.DebtInfo;
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(panelRemovedListener);

        ScreenAnimationEventHub.Instance.call_ResolveDebt(debtInfo);
    }
    public override bool exitConditionMet() {
        return panelRemoved;
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(panelRemovedListener);
    }
    public override State getNextState() {
        return allStates.getState<PostRaiseMoneyState>();
    }
    #endregion



    #region private
    private void panelRemovedListener() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION,
            () => panelRemoved = true
        );
    }
    #endregion
}
