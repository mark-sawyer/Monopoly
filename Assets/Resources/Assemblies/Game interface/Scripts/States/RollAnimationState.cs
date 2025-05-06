
public class RollAnimationState : State {
    private GamePlayer gamePlayer;
    private TokenVisualManager tokenVisualManager;
    private bool animationOver;



    #region GameState
    public override void enterState() {
        gamePlayer.rollDice();
        animationOver = false;
        int turnIndex = GameState.game.getIndexOfTurnPlayer();
        TokenScaler turnTokenScaler = tokenVisualManager.getTokenScaler(turnIndex);
        TokenVisual turnTokenVisual = tokenVisualManager.getTokenVisual(turnIndex);
        turnTokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        turnTokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion



    #region public
    public RollAnimationState(DieVisual dieVisual, TokenVisualManager tokenVisuals, GamePlayer gamePlayer) {
        dieVisual.listenForAnimationOver(animationOverCalled);
        this.tokenVisualManager = tokenVisuals;
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region private
    private void animationOverCalled() {
        animationOver = true;
    }
    #endregion
}
