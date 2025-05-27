using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveTurnState")]
public class ResolveTurnState : State {
    [SerializeField] private GameEvent turnOver;

    public override bool exitConditionMet() {
        return true;
    }
    public override void exitState() {
        turnOver.invoke();
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
}
