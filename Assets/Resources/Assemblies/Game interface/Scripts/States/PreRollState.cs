using UnityEngine;
using UnityEngine.UI;

public class PreRollState : State {
    private GamePlayer gamePlayer;
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
        gamePlayer.turn();
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }



    /* public */
    public PreRollState(GamePlayer gamePlayer, Button rollButton) {
        this.gamePlayer = gamePlayer;
        this.rollButton = rollButton;
        rollButton.onClick.AddListener(rollButtonListener);
    }



    /* private */
    private void rollButtonListener() {
        rollButtonClicked = true;
    }
}
