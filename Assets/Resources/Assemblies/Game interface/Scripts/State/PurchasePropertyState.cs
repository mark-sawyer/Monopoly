using UnityEngine;

[CreateAssetMenu(menuName = "State/PurchasePropertyState")]
public class PurchasePropertyState : State {
    public override bool exitConditionMet() {
        return false;
    }
    public override State getNextState() {
        return new PurchasePropertyState();
    }
}
