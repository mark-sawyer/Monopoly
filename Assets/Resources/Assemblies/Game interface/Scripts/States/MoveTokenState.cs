using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveTokenState : State {
    private GamePlayer gamePlayer;
    private Transform spacesParent;
    private TokenVisualManager tokenVisualManager;
    private TokenVisual tokenVisual;
    private bool tokenMadeIt;
    private int waitFrames;



    #region GameState
    public override void enterState() {
        gamePlayer.moveTurnPlayerDiceValues();

        tokenMadeIt = false;
        waitFrames = 30;

        tokenVisual = tokenVisualManager.getTurnTokenVisual();
        tokenVisual.listenForMadeItToTargetSpace(tokenMadeItCalled);

        updateTokenVisualTargetSpace(tokenVisual);
    }
    public override void update() {
        if (tokenMadeIt) {
            waitFrames -= 1;
        }
    }
    public override bool exitConditionMet() {
        return waitFrames <= 0;
    }
    public override void exitState() {
        tokenVisual.removeListenersForMadeItToTargetSpace();
        tokenVisual.changeLayer(false);
        shrinkTokenVisualsOnSpace();
    }
    public override State getNextState() {
        return possibleNextStates[0];
    }
    #endregion



    #region public
    public MoveTokenState(Transform spacesParent, TokenVisualManager tokenVisuals, GamePlayer gamePlayer) {
        this.spacesParent = spacesParent;
        this.tokenVisualManager = tokenVisuals;
        this.gamePlayer = gamePlayer;
    }
    #endregion



    #region private
    private void updateTokenVisualTargetSpace(TokenVisual tokenVisual) {
        int spaceIndex = GameState.game.getTurnPlayer().getSpaceIndex();
        SpaceVisual spaceVisual = spacesParent.GetChild(spaceIndex).GetComponent<SpaceVisual>();
        tokenVisual.updateTargetSpace(spaceVisual);
    }
    private void tokenMadeItCalled() {
        tokenMadeIt = true;
    }
    private void shrinkTokenVisualsOnSpace() {
        int spaceIndex = GameState.game.getSpaceIndexOfTurnPlayer();
        IEnumerable<TokenVisual> tokenVisualsOnSpace = tokenVisualManager.getTokenVisualsOnSpace(spaceIndex);
        float scale = UIUtilities.scaleForTokens(tokenVisualsOnSpace.Count());
        foreach (TokenVisual tv in tokenVisualsOnSpace) {
            tv.startChangingScale(scale);
        }
    }
    #endregion
}
