using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveJailDebtState")]
internal class ResolveJailDebtState : State {
    #region State
    public override bool exitConditionMet() {
        return false;
    }
    public override State getNextState() {
        throw new System.NotImplementedException();
    }
    #endregion
}
