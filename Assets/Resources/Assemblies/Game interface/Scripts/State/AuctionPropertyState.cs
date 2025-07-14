using UnityEngine;

[CreateAssetMenu(menuName = "State/AuctionPropertyState")]
public class AuctionPropertyState : State {
    #region State
    public override void enterState() {
        UIEventHub.Instance.call_FadeScreenCoverIn(255f);
    }
    public override void update() {
        Debug.Log("auction");
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override void exitState() {

    }
    public override State getNextState() {
        throw new System.NotImplementedException();
    }
    #endregion



    #region private

    #endregion
}
