using UnityEngine;

[CreateAssetMenu(menuName = "State/EliminatePlayerState")]
internal class EliminatePlayerState : State {
    #region State
    public override void enterState() {
        Debug.Log("EliminatePlayerState");
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion
}
