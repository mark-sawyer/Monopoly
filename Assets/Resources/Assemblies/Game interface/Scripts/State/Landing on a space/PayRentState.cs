using UnityEngine;

[CreateAssetMenu(menuName = "State/PayRentState")]
public class PayRentState : State {
    private bool animationOver;



    #region State
    public override void enterState() {
        animationOver = false;
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        PlayerInfo owner = propertyInfo.Owner;
        int rent = propertyInfo.Rent;
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
        ScreenAnimationEventHub.Instance.call_PayingRentAnimationBegins(GameState.game.TurnPlayer.Debt);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    #endregion



    private void animationOverCalled() {
        animationOver = true;
    }
}
