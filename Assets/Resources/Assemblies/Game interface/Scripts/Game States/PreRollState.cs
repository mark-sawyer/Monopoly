using UnityEngine;
using UnityEngine.UI;

public class PreRollState : GameState {
    private Button rollButton;
    private bool rollButtonClicked;



    /* GameState */
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
        game.turn();
    }
    public override GameState getNextState() {
        return possibleNextState[0];
    }



    /* public */
    public PreRollState(Game game, Button rollButton) : base(game) {
        this.rollButton = rollButton;
        rollButton.onClick.AddListener(rollButtonListener);
    }



    /* private */
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
}
