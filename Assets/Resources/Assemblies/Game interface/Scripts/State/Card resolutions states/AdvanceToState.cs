using UnityEngine;

[CreateAssetMenu(menuName = "State/AdvanceToState")]
public class AdvanceToState : State {
    [SerializeField] private GameEvent cardResolve;
    [SerializeField] private GameEvent tokenSettledEvent;
    private bool tokenSettled;



    #region State
    public override void enterState() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        cardResolve.invoke();
        int newSpaceIndex = turnPlayer.SpaceIndex;
        int spacesMoved = (newSpaceIndex - oldSpaceIndex).mod(GameConstants.TOTAL_SPACES);

        TokenVisualManager tokenVisualManager = TokenVisualManager.Instance;
        int turnPlayerIndex = GameState.game.IndexOfTurnPlayer;
        TokenMover tokenMover = tokenVisualManager.getTokenMover(turnPlayerIndex);
        TokenScaler tokenScaler = tokenVisualManager.getTokenScaler(turnPlayerIndex);
        TokenVisual tokenVisual = tokenVisualManager.getTokenVisual(turnPlayerIndex);
        tokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        tokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);

        tokenSettled = false;
        tokenSettledEvent.Listeners += heardTokenSettle;
        WaitFrames.Instance.exe(
            InterfaceConstants.FRAMES_FOR_TOKEN_GROWING,
            tokenMover.startMoving,
            oldSpaceIndex, spacesMoved
        );
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenSettledEvent.Listeners -= heardTokenSettle;
    }
    public override State getNextState() {
        SpaceInfo spaceInfo = GameState.game.SpaceInfoOfTurnPlayer;

        if (spaceInfo is PropertySpaceInfo propertySpaceInfo) {
            PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
            if (!propertyInfo.IsBought) return allStates.getState<BuyPropertyOptionState>();
            else if (propertyInfo.Owner != GameState.game.TurnPlayer) return allStates.getState<PayRentState>();
        }
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
