using UnityEngine;

[CreateAssetMenu(menuName = "State/NextUtilityState")]
internal class NextUtilityState : State {
    private UtilityInfo utilityInfo;
    private bool goToResolveDebt;
    private bool goToLandedOnSpace;


    #region State
    public override void enterState() {
        goToResolveDebt = false;
        goToLandedOnSpace = false;

        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);
        ScreenOverlayEventHub.Instance.sub_RemoveScreenOverlay(animationOverCalled);


        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int spacesToMove = getSpacesToNextUtility(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        utilityInfo = (UtilityInfo)((PropertySpaceInfo)newSpace).PropertyInfo;

        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedToSpace(newSpace, oldSpaceIndex);
        DataEventHub.Instance.call_CardResolved();
    }
    public override bool exitConditionMet() {
        return goToResolveDebt
            || goToLandedOnSpace;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenOverlay(animationOverCalled);
    }
    public override State getNextState() {
        if (goToResolveDebt) return allStates.getState<ResolveDebtState>();
        else return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        bool tenTimesDiceRentRequired = utilityInfo.IsBought && utilityInfo.Owner != GameState.game.TurnPlayer;
        if (tenTimesDiceRentRequired) {
            PlayerInfo owner = utilityInfo.Owner;
            int rent = 10 * GameState.game.DiceInfo.TotalValue;
            DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
            SingleCreditorDebtInfo debtInfo = (SingleCreditorDebtInfo)GameState.game.TurnPlayer.DebtInfo;
            ScreenOverlayEventHub.Instance.call_PayingRentAnimationBegins(debtInfo);
        }
        else {
            goToLandedOnSpace = true;
        }
    }
    private void animationOverCalled() {
        goToResolveDebt = true;
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
    #endregion
}
