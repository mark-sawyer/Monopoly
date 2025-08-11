using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/RaiseMoneyState")]
internal class RaiseMoneyState : State {
    private bool panelRemoved;



    #region State
    public override void enterState() {
        panelRemoved = false;
        ScreenOverlayEventHub.Instance.sub_RemoveScreenOverlay(panelRemovedListener);

        DebtInfo debtInfo = GameState.game.PlayerInDebt.DebtInfo;
        ScreenOverlayEventHub.Instance.call_ResolveDebt(debtInfo);
    }
    public override bool exitConditionMet() {
        return panelRemoved;
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenOverlay(panelRemovedListener);
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
