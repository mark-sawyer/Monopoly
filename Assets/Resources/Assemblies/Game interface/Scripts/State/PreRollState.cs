using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    [SerializeField] private GameEvent regularTurnBegin;
    [SerializeField] private GameEvent rollButtonClickedEvent;
    [SerializeField] private GameEvent managePropertiesClickedEvent;
    private bool rollButtonClicked;
    private bool managePropertiesClicked;



    #region GameState
    public override void enterState() {
        rollButtonClicked = false;
        managePropertiesClicked = false;
        rollButtonClickedEvent.Listeners += rollButtonListener;
        managePropertiesClickedEvent.Listeners += managePropertiesListener;
        regularTurnBegin.invoke();
    }
    public override bool exitConditionMet() {
        return rollButtonClicked
            || managePropertiesClicked;
    }
    public override void exitState() {
        rollButtonClickedEvent.Listeners -= rollButtonListener;
        managePropertiesClickedEvent.Listeners -= managePropertiesListener;
    }
    public override State getNextState() {
        if (rollButtonClicked) return allStates.getState<RollAnimationState>();
        if (managePropertiesClicked) return allStates.getState<ManagePropertiesState>();
        throw new System.Exception();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
    private void managePropertiesListener() {
        managePropertiesClicked = true;
    }
    #endregion
}
