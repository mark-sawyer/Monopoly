using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveJailDebtState")]
public class ResolveJailDebtState : State {
    #region State
    public override void update() {
        Debug.Log("ResolveJailDebtState");
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override State getNextState() {
        throw new System.NotImplementedException();
    }
    #endregion
}
