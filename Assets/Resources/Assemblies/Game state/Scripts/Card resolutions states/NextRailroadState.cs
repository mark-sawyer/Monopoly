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
        ScreenOverlayEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int spacesToMove = getSpacesToNextRailroad(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        railroadInfo = (RailroadInfo)((PropertySpaceInfo)newSpace).PropertyInfo;

        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedToSpace(newSpace, oldSpaceIndex);
        DataEventHub.Instance.call_CardResolved();

    }
    public override bool exitConditionMet() {
        return goToResolveDebt || goToLandedOnSpace;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenOverlayEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    public override State getNextState() {
        if (goToResolveDebt) return allStates.getState<ResolveDebtState>();
        else return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        bool doubleRentRequired = railroadInfo.IsBought && railroadInfo.Owner != GameState.game.TurnPlayer;
        if (doubleRentRequired) {
            PlayerInfo owner = railroadInfo.Owner;
            int rent = 2 * railroadInfo.Rent;
            DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
            ScreenOverlayEventHub.Instance.call_PayingRentAnimationBegins(GameState.game.TurnPlayer.DebtInfo);
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
