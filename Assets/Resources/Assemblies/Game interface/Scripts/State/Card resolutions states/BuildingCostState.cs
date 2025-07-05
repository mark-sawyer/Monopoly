using UnityEngine;

[CreateAssetMenu(menuName = "State/BuildingCostState")]
public class BuildingCostState : State {
    public override bool exitConditionMet() {
        throw new System.NotImplementedException();
    }

    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
}
