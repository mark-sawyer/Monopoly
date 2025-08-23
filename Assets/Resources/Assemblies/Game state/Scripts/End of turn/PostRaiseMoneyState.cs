using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PostRaiseMoneyState")]
internal class PostRaiseMoneyState : State {
    private bool updateAnimationsOver;



    #region State
    public override void enterState() {
        updateAnimationsOver = false;
        UIEventHub.Instance.sub_AllExpiredPropertyVisualsUpdated(updateAnimationsOverListener);

        PlayerInfo[] playersNeedingMoneyUIUpdate = PlayerPanelManager.Instance.getPlayersNeedingMoneyUIUpdate();
        if (playersNeedingMoneyUIUpdate.Length > 0) {
            UIEventHub.Instance.call_UpdateUIMoney(playersNeedingMoneyUIUpdate);
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE,
                () => UIEventHub.Instance.call_UpdateIconsAfterResolveDebt()
            );
        }
        else {
            UIEventHub.Instance.call_UpdateIconsAfterResolveDebt();
        }
    }
    public override bool exitConditionMet() {
        return updateAnimationsOver;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_AllExpiredPropertyVisualsUpdated(updateAnimationsOverListener);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    #region private
    private void updateAnimationsOverListener() {
        WaitFrames.Instance.beforeAction(
            50,
            () => updateAnimationsOver = true
        );
    }
    #endregion
}
