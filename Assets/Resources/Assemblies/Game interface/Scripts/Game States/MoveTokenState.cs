using UnityEngine;

public class MoveTokenState : GameState {
    private GetMovingTokenInformation getMovingTokenInformation;



    public MoveTokenState(Game game, GetMovingTokenInformation getMovingTokenInformation) : base(game) {
        this.getMovingTokenInformation = getMovingTokenInformation;
    }



    public override void update() {
        Debug.Log("MoveTokenState");
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override GameState getNextState() {
        return possibleNextState[0];
    }
}
