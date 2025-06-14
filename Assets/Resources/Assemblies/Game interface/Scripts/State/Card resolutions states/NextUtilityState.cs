using UnityEngine;

[CreateAssetMenu(menuName = "State/NextUtilityState")]
public class NextUtilityState : State {
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private GameEvent tokenSettledEvent;
    [SerializeField] private GameEvent rentAnimationOver;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    [SerializeField] private DebtEvent payingRentAnimationBegins;
    private UtilityInfo utilityInfo;
    private bool tenTimesDiceRentResolved;
    private bool tokenSettled;
    private bool tenTimesDiceRentRequired;
    private bool animationStarted;


    #region State
    public override void enterState() {
        tokenSettledEvent.Listeners += heardTokenSettle;
        rentAnimationOver.Listeners += animationOverCalled;
        tenTimesDiceRentResolved = true;
        tokenSettled = false;
        animationStarted = false;

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int spacesToMove = getSpacesToNextUtility(oldSpaceIndex);
        int newSpaceIndex = (oldSpaceIndex + spacesToMove) % GameConstants.TOTAL_SPACES;
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        turnPlayerMovedToSpace.invoke(newSpace);
        cardResolved.invoke();

        utilityInfo = (UtilityInfo)((PropertySpaceInfo)newSpace).PropertyInfo;
        tenTimesDiceRentRequired = getTenTimesDiceRequired(turnPlayer, utilityInfo);
        if (tenTimesDiceRentRequired) tenTimesDiceRentResolved = false;

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
        tokenSettledEvent.Listeners -= heardTokenSettle;
        rentAnimationOver.Listeners -= animationOverCalled;
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
    private void growToken(int turnPlayerIndex) {
        TokenScaler tokenScaler = TokenVisualManager.Instance.getTokenScaler(turnPlayerIndex);
        TokenVisual tokenVisual = TokenVisualManager.Instance.getTokenVisual(turnPlayerIndex);
        tokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        tokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
    }
    private void startRentAnimation() {
        PlayerInfo owner = utilityInfo.Owner;
        int rent = 10 * GameState.game.DiceInfo.TotalValue;
        playerIncurredDebt.invoke(GameState.game.TurnPlayer, owner, rent);
        payingRentAnimationBegins.invoke(GameState.game.TurnPlayer.Debt);
    }
    #endregion
}
