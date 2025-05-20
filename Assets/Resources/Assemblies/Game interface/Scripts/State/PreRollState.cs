using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "State/PreRollState")]
public class PreRollState : State {
    [SerializeField] GameEvent rollButtonClickedEvent;
    private Button rollButton;
    private bool rollButtonClicked;



    #region GameState
    public override void enterState() {
        rollButton.enabled = true;
        rollButtonClicked = false;
        rollButtonClickedEvent.Listeners += rollButtonListener;
    }
    public override void update() {
        Debug.Log("PreRollState");
    }
    public override bool exitConditionMet() {
        return rollButtonClicked;
    }
    public override void exitState() {
        rollButton.enabled = false;
        rollButtonClickedEvent.Listeners -= rollButtonListener;
    }
    public override State getNextState() {
        return possibleNextStates[0];
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
