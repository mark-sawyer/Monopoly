
using UnityEngine;

[CreateAssetMenu(menuName = "State/PostManagePropertiesClosedState")]
internal class PostManagePropertiesClosedState : State {
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
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
    #endregion



    #region private
    private void updateAnimationsOverListener() {
        updateAnimationsOver = true;
    }
    #endregion
}
