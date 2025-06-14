using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerMoneyCard")]
public class PlayerMoneyCard : State {
    [SerializeField] private GameEvent cardResolved;
    public override bool exitConditionMet() {
        throw new System.NotImplementedException();
    }

    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
}
