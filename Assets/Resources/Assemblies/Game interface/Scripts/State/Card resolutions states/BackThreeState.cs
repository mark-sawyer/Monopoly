using UnityEngine;

[CreateAssetMenu(menuName = "State/BackThreeState")]
public class BackThreeState : State {
    [SerializeField] private SpaceEvent turnPlayerMovedToSpace;
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private GameEvent tokenSettledEvent;
    private bool tokenSettled;



    #region State
    public override void enterState() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int newSpaceIndex = (oldSpaceIndex - 3).mod(GameConstants.TOTAL_SPACES);
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        turnPlayerMovedToSpace.invoke(newSpace);
        cardResolved.invoke();

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
            tokenMover.startMovingDirectly,
            oldSpaceIndex, newSpaceIndex
        );
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenSettledEvent.Listeners -= heardTokenSettle;
    }
    public override State getNextState() {
        return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
