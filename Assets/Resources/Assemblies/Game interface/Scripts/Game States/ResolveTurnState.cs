using UnityEngine;

public class ResolveTurnState : GameState {
    public ResolveTurnState(Game game) : base(game) { }



    public override void update() {
        Debug.Log("ResolveTurnState");
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override GameState getNextState() {
        return possibleNextState[0];
    }
}
