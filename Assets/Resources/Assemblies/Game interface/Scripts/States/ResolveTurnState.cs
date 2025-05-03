using UnityEngine;

public class ResolveTurnState : State {
    private GamePlayer gamePlayer;



    #region GameState
    public override void update() {
        Debug.Log("ResolveTurnState");
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override void exitState() {
        gamePlayer.updateTurnPlayer();
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion



    #region public
    public ResolveTurnState(GamePlayer gamePlayer) {
        this.gamePlayer = gamePlayer;
    }
    #endregion
}
