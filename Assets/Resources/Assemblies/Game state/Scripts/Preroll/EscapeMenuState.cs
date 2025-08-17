using UnityEngine;

[CreateAssetMenu(menuName = "State/EscapeMenuState")]
internal class EscapeMenuState : State {
    private bool continueClicked;



    #region State
    public override void enterState() {
        continueClicked = false;
        ScreenOverlayEventHub.Instance.sub_ContinueClicked(continueClickedListener);

        SoundOnlyEventHub.Instance.call_Dub();
        ScreenOverlayEventHub.Instance.call_EscapeMenu();
    }
    public override bool exitConditionMet() {
        return continueClicked;
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_ContinueClicked(continueClickedListener);
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
    #endregion



    private void continueClickedListener() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION,
            () => continueClicked = true
        );
    }
}
