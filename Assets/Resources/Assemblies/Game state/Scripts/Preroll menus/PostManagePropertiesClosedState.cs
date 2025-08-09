
using UnityEngine;

[CreateAssetMenu(menuName = "State/PostManagePropertiesClosedState")]
internal class PostManagePropertiesClosedState : State {
    private bool updateAnimationsOver;



    #region State
    public override void enterState() {
        updateAnimationsOver = false;
        UIEventHub.Instance.sub_AllExpiredPropertyVisualsUpdated(updateAnimationsOverListener);
        UIEventHub.Instance.call_UpdateExpiredPropertyVisuals();
    }
    public override bool exitConditionMet() {
        return updateAnimationsOver;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_AllExpiredPropertyVisualsUpdated(updateAnimationsOverListener);
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
