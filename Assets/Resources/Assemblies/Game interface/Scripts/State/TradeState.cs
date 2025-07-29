
using UnityEngine;

[CreateAssetMenu(menuName = "State/TradeState")]
public class TradeState : State {
    #region State
    public override void enterState() {

    }
    public override bool exitConditionMet() {
        return false;
    }
    public override State getNextState() {
        throw new System.NotImplementedException();
    }
    #endregion
}
