using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveTokenState : State {
    private GamePlayer gamePlayer;
    private SpaceVisualManager spacesVisualManager;
    private TokenVisualManager tokenVisualManager;
    private TokenVisual tokenVisual;



    #region GameState
    public override void enterState() {
        gamePlayer.moveTurnPlayerDiceValues();

        tokenVisual = tokenVisualManager.getTurnTokenVisual();
        //tokenVisual.listenForMadeItToTargetSpace(tokenMadeItCalled);

        updateTokenVisualTargetSpace(tokenVisual);
    }
    public override bool exitConditionMet() {
        return false;
    }
    public override void exitState() {
        //tokenVisual.removeListenersForMadeItToTargetSpace();
        tokenVisual.changeLayer(false);
        shrinkTokenVisualsOnSpace();
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion



    #region public
    public MoveTokenState(SpaceVisualManager spacesVisualManager, TokenVisualManager tokenVisualManager, GamePlayer gamePlayer) {
        this.spacesVisualManager = spacesVisualManager;
        this.tokenVisualManager = tokenVisualManager;
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region private
    private void updateTokenVisualTargetSpace(TokenVisual tokenVisual) {
        int spaceIndex = GameState.game.getTurnPlayer().getSpaceIndex();
        SpaceVisual spaceVisual = spacesVisualManager.getSpaceVisual(spaceIndex);
        //tokenVisual.updateTargetSpace(spaceVisual);
    }
    private void tokenMadeItCalled() {
        //tokenMadeIt = true;
    }
    private void shrinkTokenVisualsOnSpace() {
        //int spaceIndex = GameState.game.getSpaceIndexOfTurnPlayer();
        //IEnumerable<TokenVisual> tokenVisualsOnSpace = tokenVisualManager.getTokenVisualsOnSpace(spaceIndex);
        //float scale = UIUtilities.scaleForTokens(tokenVisualsOnSpace.Count());
        //foreach (TokenVisual tv in tokenVisualsOnSpace) {
        //    tv.startChangingScale(scale);
        //}
    }
    #endregion
}
