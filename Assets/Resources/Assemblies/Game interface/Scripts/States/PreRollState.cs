using UnityEngine;
using UnityEngine.UI;

public class PreRollState : State {
    private Button rollButton;
    private bool rollButtonClicked;



    #region GameState
    public override void enterState() {
        rollButton.enabled = true;
        rollButtonClicked = false;
    }
    public override void update() {
        Debug.Log("PreRollState");
    }
    public override bool exitConditionMet() {
        return rollButtonClicked;
    }
    public override void exitState() {
        rollButton.enabled = false;
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion



    #region public
    public PreRollState(Button rollButton) {
        this.rollButton = rollButton;
        rollButton.onClick.AddListener(rollButtonListener);
    }
    #endregion



    #region private
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
    #endregion
}
