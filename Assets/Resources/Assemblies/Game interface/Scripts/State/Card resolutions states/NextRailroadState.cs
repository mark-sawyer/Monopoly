using UnityEngine;

[CreateAssetMenu(menuName = "State/NextRailroadState")]
public class NextRailroadState : State {
    private RailroadInfo railroadInfo;
    private bool doubleRentResolved;
    private bool tokenSettled;
    private bool doubleRentRequired;
    private bool animationStarted;


    #region State
    public override void enterState() {
        initialise();

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int spacesToMove = getSpacesToNextRailroad(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedToSpace(newSpace, oldSpaceIndex);
        DataEventHub.Instance.call_CardResolved();

        railroadInfo = (RailroadInfo)((PropertySpaceInfo)newSpace).PropertyInfo;
        doubleRentRequired = getDoubleRentRequired(turnPlayer, railroadInfo);
        if (doubleRentRequired) doubleRentResolved = false;
    }
    public override void update() {
        if (!tokenSettled) return;
        if (!doubleRentRequired) return;
        if (!animationStarted) {
            startRentAnimation();
            animationStarted = true;
        }
    }
    public override bool exitConditionMet() {
        return tokenSettled && doubleRentResolved;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    public override State getNextState() {
        if (doubleRentRequired) return allStates.getState<ResolveDebtState>();
        else return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void initialise() {
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        doubleRentResolved = true;
        tokenSettled = false;
        animationStarted = false;
    }
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    private void animationOverCalled() {
        doubleRentResolved = true;
    }
    private int getSpacesToNextRailroad(int oldSpaceIndex) {
        int mod10 = oldSpaceIndex % GameConstants.SPACES_ON_EDGE;
        return (5 - mod10).mod(GameConstants.SPACES_ON_EDGE);
    }
    private bool getDoubleRentRequired(PlayerInfo turnPlayer, RailroadInfo railroadInfo) {
        return railroadInfo.IsBought && railroadInfo.Owner != turnPlayer;
    }
    private void startRentAnimation() {
        PlayerInfo owner = railroadInfo.Owner;
        int rent =  2 * railroadInfo.Rent;
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, owner, rent);
        ScreenAnimationEventHub.Instance.call_PayingRentAnimationBegins(GameState.game.TurnPlayer.Debt);
    }
    #endregion
}
