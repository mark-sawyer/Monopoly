using UnityEngine;

[CreateAssetMenu(menuName = "State/PayRentState")]
public class PayRentState : State {
    [SerializeField] private DebtEvent payingRentAnimationBegins;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    private bool animationOver;



    #region State
    public override void enterState() {
        animationOver = false;
        ScreenAnimation.removeScreenAnimation.Listeners += animationOverCalled;
        PropertySpaceInfo propertySpaceInfo = (PropertySpaceInfo)GameState.game.SpaceInfoOfTurnPlayer;
        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        PlayerInfo owner = propertyInfo.Owner;
        int rent = propertyInfo.Rent;
        playerIncurredDebt.invoke(GameState.game.TurnPlayer, owner, rent);
        payingRentAnimationBegins.invoke(GameState.game.TurnPlayer.Debt);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    public override void exitState() {
        ScreenAnimation.removeScreenAnimation.Listeners -= animationOverCalled;
    }
    #endregion



    private void animationOverCalled() {
        animationOver = true;
    }
}
