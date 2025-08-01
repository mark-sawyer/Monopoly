using UnityEngine;

[CreateAssetMenu(menuName = "State/GoToJailCardState")]
public class GoToJailCardState : State {
    public override bool exitConditionMet() {
        throw new System.NotImplementedException();
    }

    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
}
