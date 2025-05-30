using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    [SerializeField] GameEvent rollButtonClickedEvent;
    private Button rollButton;
    private bool rollButtonClicked;



    #region GameState
    public override void enterState() {
        rollButton.interactable = true;
        rollButtonClicked = false;
        rollButtonClickedEvent.Listeners += rollButtonListener;
    }
    public override bool exitConditionMet() {
        return rollButtonClicked;
    }
    public override void exitState() {
        rollButton.interactable = false;
        rollButtonClickedEvent.Listeners -= rollButtonListener;
    }
    public override State getNextState() {
        return getState<RollAnimationState>();
    }
    #endregion



    #region public
    public void setup(Button rollButton) {
        this.rollButton = rollButton;
    }
    #endregion



    #region private
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
    #endregion
}
