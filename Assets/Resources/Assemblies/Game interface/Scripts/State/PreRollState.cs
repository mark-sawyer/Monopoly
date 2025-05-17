using System;
using UnityEngine;
using UnityEngine.UI;

public class PreRollState : State {
    private Button rollButton;
    [SerializeField] private bool rollButtonClicked = false;



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
    public void setup(Button rollButton) {
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
