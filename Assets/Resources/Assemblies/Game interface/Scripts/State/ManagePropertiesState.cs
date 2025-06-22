using UnityEngine;

[CreateAssetMenu(menuName = "State/ManagePropertiesState")]
public class ManagePropertiesState : State {
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
