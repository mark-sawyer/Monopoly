
using UnityEngine;

[CreateAssetMenu(menuName = "State/PostManagePropertiesClosedState")]
public class PostManagePropertiesClosedState : State {
    private bool updateAnimationsOver;



    #region State
    public override void enterState() {
        updateAnimationsOver = false;
        ManagePropertiesEventHub.Instance.sub_AllVisualsUpdatedAfterManagePropertiesClosed(updateAnimationsOverListener);
        ManagePropertiesEventHub.Instance.call_UpdateIconsAfterManagePropertiesClosed();
    }
    public override bool exitConditionMet() {
        return updateAnimationsOver;
    }
    public override void exitState() {
        ManagePropertiesEventHub.Instance.unsub_AllVisualsUpdatedAfterManagePropertiesClosed(updateAnimationsOverListener);
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
    private void updateAnimationsOverListener() {
        updateAnimationsOver = true;
    }
    #endregion
}
