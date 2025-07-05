using UnityEngine;

[CreateAssetMenu(menuName = "State/NextUtilityState")]
public class NextUtilityState : State {
    private UtilityInfo utilityInfo;
    private bool tenTimesDiceRentResolved;
    private bool tokenSettled;
    private bool tenTimesDiceRentRequired;
    private bool animationStarted;


    #region State
    public override void enterState() {
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        tenTimesDiceRentResolved = true;
        tokenSettled = false;
        animationStarted = false;

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int spacesToMove = getSpacesToNextUtility(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        DataEventHub.Instance.call_TurnPlayerMovedToSpace(newSpace, oldSpaceIndex);
        DataEventHub.Instance.call_CardResolved();

        utilityInfo = (UtilityInfo)((PropertySpaceInfo)newSpace).PropertyInfo;
        tenTimesDiceRentRequired = getTenTimesDiceRequired(turnPlayer, utilityInfo);
        if (tenTimesDiceRentRequired) tenTimesDiceRentResolved = false;
    }
    public override void update() {
        if (!tokenSettled) return;
        if (!tenTimesDiceRentRequired) return;
        if (!animationStarted) {
            startRentAnimation();
            animationStarted = true;
        }
    }
    public override bool exitConditionMet() {
        return tokenSettled && tenTimesDiceRentResolved;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    public override State getNextState() {
        if (tenTimesDiceRentRequired) return allStates.getState<ResolveDebtState>();
        else return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    private void animationOverCalled() {
        tenTimesDiceRentResolved = true;
    }
    private int getSpacesToNextUtility(int oldSpaceIndex) {
        if (oldSpaceIndex < GameConstants.ELECTRIC_COMPANY_SPACE_INDEX) {
            return GameConstants.ELECTRIC_COMPANY_SPACE_INDEX - oldSpaceIndex;
        }
        else if (oldSpaceIndex < GameConstants.WATER_WORKS_SPACE_INDEX) {
            return GameConstants.WATER_WORKS_SPACE_INDEX - oldSpaceIndex;
        }
        else {
            return (GameConstants.ELECTRIC_COMPANY_SPACE_INDEX - oldSpaceIndex).mod(GameConstants.TOTAL_SPACES);
        }
    }
    private bool getTenTimesDiceRequired(PlayerInfo turnPlayer, UtilityInfo utilityInfo) {
        return utilityInfo.IsBought && utilityInfo.Owner != turnPlayer;
    }
    private void startRentAnimation() {
        PlayerInfo owner = utilityInfo.Owner;
        int rent = 10 * GameState.game.DiceInfo.TotalValue;
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
        ScreenAnimationEventHub.Instance.call_PayingRentAnimationBegins(GameState.game.TurnPlayer.Debt);
    }
    #endregion
}
