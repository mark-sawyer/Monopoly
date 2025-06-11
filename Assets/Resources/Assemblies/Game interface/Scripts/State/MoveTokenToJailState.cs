using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenToJail")]
public class MoveTokenToJailState : State {
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private GameEvent tokenSettledEvent;
    private bool tokenSettled;


    #region GameState
    public override void enterState() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenMover tokenMover = TokenVisualManager.Instance.getTokenMover(turnIndex);
        TokenVisual tokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        TokenScaler tokenScaler = TokenVisualManager.Instance.getTokenScaler(turnIndex);
        tokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
        int startingIndex = GameState.game.SpaceIndexOfTurnPlayer;
        turnPlayerSentToJail.invoke();
        tokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        tokenSettled = false;
        tokenSettledEvent.Listeners += heardTokenSettle;
        WaitFrames.Instance.exe(
            InterfaceConstants.FRAMES_FOR_TOKEN_GROWING,
            tokenMover.startMovingDirectly,
            startingIndex, GameConstants.JAIL_SPACE_INDEX
        );
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenSettledEvent.Listeners -= heardTokenSettle;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
