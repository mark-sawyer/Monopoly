using UnityEngine;

[CreateAssetMenu(menuName = "State/NextRailroadState")]
internal class NextRailroadState : State {
    private RailroadInfo railroadInfo;
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
        int spacesToMove = getSpacesToNextRailroad(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        railroadInfo = (RailroadInfo)((PropertySpaceInfo)newSpace).PropertyInfo;


        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedAlongBoard(oldSpaceIndex, spacesToMove);
        DataEventHub.Instance.call_CardResolved();

    }
    public override bool exitConditionMet() {
        return goToResolveDebt || goToLandedOnSpace;
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
        bool doubleRentRequired = railroadInfo.IsBought && !railroadInfo.IsMortgaged && railroadInfo.Owner != turnPlayer;
        if (doubleRentRequired) {
            PlayerInfo owner = railroadInfo.Owner;
            int rent = 2 * railroadInfo.Rent;
            DataEventHub.Instance.call_PlayerIncurredDebt(turnPlayer, owner, rent);
            SingleCreditorDebtInfo debtInfo = (SingleCreditorDebtInfo)turnPlayer.DebtInfo;
            ScreenOverlayStarterEventHub.Instance.call_PayingRentAnimationBegins(debtInfo);
        }
        else {
            goToLandedOnSpace = true;
        }
    }
    private void animationOverCalled() {
        goToResolveDebt = true;
    }
    private int getSpacesToNextRailroad(int oldSpaceIndex) {
        int mod10 = oldSpaceIndex % GameConstants.SPACES_ON_EDGE;
        return (5 - mod10).mod(GameConstants.SPACES_ON_EDGE);
    }
    #endregion
}
