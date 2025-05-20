using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveTurnState")]
public class ResolveTurnState : State {
    public override void update() {
        Debug.Log("ResolveTurnState");
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
}
