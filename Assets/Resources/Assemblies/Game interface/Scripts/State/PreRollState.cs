using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    [SerializeField] GameEvent rollButtonClickedEvent;
    private bool rollButtonClicked;



    #region GameState
    public override void enterState() {
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
