using UnityEngine;

[CreateAssetMenu(menuName = "State/ManagePropertiesState")]
internal class ManagePropertiesState : State {
    private bool managePropertiesPanelRaised;



    #region State
    public override void enterState() {
        managePropertiesPanelRaised = false;
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(backButtonListening);
    }
    public override bool exitConditionMet() {
        return managePropertiesPanelRaised;
    }
    public override void exitState() {
        ManagePropertiesEventHub.Instance.unsub_BackButtonPressed(backButtonListening);
    }
    public override State getNextState() {
        return allStates.getState<PostManagePropertiesClosedState>();
    }
    #endregion



    #region private
    private void backButtonListening() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.MANAGE_PROPERTIES_DROP,
            () => managePropertiesPanelRaised = true
        );
    }
    #endregion
}
