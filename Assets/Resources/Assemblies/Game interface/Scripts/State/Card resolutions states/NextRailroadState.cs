using UnityEngine;

[CreateAssetMenu(menuName = "State/NextRailroadState")]
public class NextRailroadState : State {
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private GameEvent tokenSettledEvent;
    [SerializeField] private GameEvent rentAnimationOver;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private DebtEvent payingRentAnimationBegins;
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
        turnPlayerMovedToSpace.invoke(newSpace);
        cardResolved.invoke();

        railroadInfo = (RailroadInfo)((PropertySpaceInfo)newSpace).PropertyInfo;
        doubleRentRequired = getDoubleRentRequired(turnPlayer, railroadInfo);
        if (doubleRentRequired) doubleRentResolved = false;

        int turnPlayerIndex = GameState.game.IndexOfTurnPlayer;
        growToken(turnPlayerIndex);

        TokenMover tokenMover = TokenVisualManager.Instance.getTokenMover(turnPlayerIndex);
        WaitFrames.Instance.exe(
            InterfaceConstants.FRAMES_FOR_TOKEN_GROWING,
            tokenMover.startMoving,
            oldSpaceIndex, spacesToMove
        );
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
        tokenSettledEvent.Listeners -= heardTokenSettle;
        rentAnimationOver.Listeners -= animationOverCalled;
    }
    public override State getNextState() {
        if (doubleRentRequired) return allStates.getState<ResolveDebtState>();
        else return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void initialise() {
        tokenSettledEvent.Listeners += heardTokenSettle;
        rentAnimationOver.Listeners += animationOverCalled;
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
    private void growToken(int turnPlayerIndex) {
        TokenScaler tokenScaler = TokenVisualManager.Instance.getTokenScaler(turnPlayerIndex);
        TokenVisual tokenVisual = TokenVisualManager.Instance.getTokenVisual(turnPlayerIndex);
        tokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        tokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
    }
    private void startRentAnimation() {
        PlayerInfo owner = railroadInfo.Owner;
        int rent =  2 * railroadInfo.Rent;
        playerIncurredDebt.invoke(GameState.game.TurnPlayer, owner, rent);
        payingRentAnimationBegins.invoke(GameState.game.TurnPlayer.Debt);
    }
    #endregion
}
