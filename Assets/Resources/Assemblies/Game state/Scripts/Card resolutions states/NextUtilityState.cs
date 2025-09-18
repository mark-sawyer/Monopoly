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
        ScreenOverlayFunctionEventHub.Instance.sub_RemoveScreenOverlay(animationOverCalled);


        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int spacesToMove = getSpacesToNextUtility(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        utilityInfo = (UtilityInfo)((PropertySpaceInfo)newSpace).PropertyInfo;


        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedAlongBoard(oldSpaceIndex, spacesToMove);
        DataEventHub.Instance.call_CardResolved();
    }
    public override bool exitConditionMet() {
        return goToResolveDebt
            || goToLandedOnSpace;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenOverlayFunctionEventHub.Instance.unsub_RemoveScreenOverlay(animationOverCalled);
    }
    public override State getNextState() {
        if (goToResolveDebt) return allStates.getState<ResolveDebtState>();
        else return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        bool tenTimesDiceRentRequired = utilityInfo.IsBought && !utilityInfo.IsMortgaged && utilityInfo.Owner != turnPlayer;
        if (tenTimesDiceRentRequired) {
            PlayerInfo owner = utilityInfo.Owner;
            int[] nonTurnRoll = GameState.game.DiceInfo.getNonTurnDiceRoll();
            int rent = 10 * (nonTurnRoll[0] + nonTurnRoll[1]);
            DataEventHub.Instance.call_PlayerIncurredDebt(turnPlayer, owner, rent);
            UIEventHub.Instance.call_NonTurnDiceRoll(nonTurnRoll[0], nonTurnRoll[1]);
            WaitFrames.Instance.beforeAction(
                InterfaceConstants.DIE_FRAMES_PER_IMAGE * InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING + 70,
                () => {
                    SingleCreditorDebtInfo debtInfo = (SingleCreditorDebtInfo)turnPlayer.DebtInfo;
                    ScreenOverlayStarterEventHub.Instance.call_PayingRentAnimationBegins(debtInfo);
                }
            );
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
