using UnityEngine;

public class RollAnimationState : State {
    private GamePlayer gamePlayer;
    private TokenVisualManager tokenVisualManager;
    private bool animationOver;



    #region GameState
    public override void enterState() {
        gamePlayer.rollDice();
        animationOver = false;
        TokenVisual turnToken = tokenVisualManager.getTurnTokenVisual();
        turnToken.startChangingScale(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        turnToken.changeLayer(true);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        TokenVisual turnToken = tokenVisualManager.getTurnTokenVisual();
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
