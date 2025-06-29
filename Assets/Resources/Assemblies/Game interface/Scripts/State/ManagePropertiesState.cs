using UnityEngine;

[CreateAssetMenu(menuName = "State/ManagePropertiesState")]
public class ManagePropertiesState : State {
    [SerializeField] private GameEvent backButtonPressedEvent;
    private bool managePropertiesPanelRaised;




    #region State
    public override void enterState() {
        managePropertiesPanelRaised = false;
        backButtonPressedEvent.Listeners += backButtonListening;
    }
    public override bool exitConditionMet() {
        return managePropertiesPanelRaised;
    }
    public override void exitState() {
        backButtonPressedEvent.Listeners -= backButtonListening;
    }
    public override State getNextState() {
        return allStates.getState<PostManagePropertiesClosedState>();
    }
    #endregion

    #region private
    private void backButtonListening() {
        WaitFrames.Instance.exe(
            InterfaceConstants.FRAMES_FOR_MANAGE_PROPERTIES_DROP,
            () => managePropertiesPanelRaised = true
        );
    }
    #endregion
}
