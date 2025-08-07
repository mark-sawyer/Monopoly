using UnityEngine;

[CreateAssetMenu(menuName = "State/GameBeginsState")]
internal class GameBeginsState : State {
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
}
