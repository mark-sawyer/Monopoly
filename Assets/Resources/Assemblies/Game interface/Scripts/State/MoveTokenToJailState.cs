using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenToJail")]
public class MoveTokenToJailState : State {
    [SerializeField] private GameEvent turnPlayerSentToJail;
    [SerializeField] private GameEvent tokenSettledEvent;
    private TokenVisual tokenVisual;
    private bool tokenSettled;


    #region GameState
    public override void enterState() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        tokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        int startingIndex = GameState.game.SpaceIndexOfTurnPlayer;
        turnPlayerSentToJail.invoke();
        tokenVisual.beginMovingToJail(startingIndex);
        tokenSettled = false;
        tokenSettledEvent.Listeners += heardTokenSettle;
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenVisual.changeLayer(InterfaceConstants.BOARD_TOKEN_LAYER_NAME);
        tokenSettledEvent.Listeners -= heardTokenSettle;
    }
    public override State getNextState() {
        return getState<ResolveTurnState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
