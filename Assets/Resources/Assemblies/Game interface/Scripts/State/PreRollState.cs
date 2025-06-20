using UnityEngine;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    [SerializeField] private GameEvent regularTurnBegin;
    [SerializeField] private GameEvent rollButtonClickedEvent;
    private bool rollButtonClicked;



    #region GameState
    public override void enterState() {
        regularTurnBegin.invoke();
        rollButtonClickedEvent.Listeners += rollButtonListener;
        rollButtonClicked = false;
    }
    public override bool exitConditionMet() {
        return rollButtonClicked;
    }
    public override void exitState() {
        rollButtonClickedEvent.Listeners -= rollButtonListener;
    }
    public override State getNextState() {
        return allStates.getState<RollAnimationState>();
    }
    #endregion



    #region private
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
    #endregion
}
