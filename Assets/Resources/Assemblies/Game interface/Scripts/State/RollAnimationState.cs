using UnityEngine;

[CreateAssetMenu(menuName = "State/RollAnimationState")]
public class RollAnimationState : State {
    [SerializeField] private GameEvent diceAnimationOver;
    private DiceInfo diceInfo;
    private TokenVisualManager tokenVisualManager;
    private bool animationOver;



    #region GameState
    public override void enterState() {
        animationOver = false;
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenScaler turnTokenScaler = tokenVisualManager.getTokenScaler(turnIndex);
        TokenVisual turnTokenVisual = tokenVisualManager.getTokenVisual(turnIndex);
        turnTokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        turnTokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override State getNextState() {
        return getState<MoveTokenState>();
    }
    #endregion



    #region public
    public void setup(TokenVisualManager tokenVisualManager) {
        diceAnimationOver.Listeners += animationOverCalled;
        this.tokenVisualManager = tokenVisualManager;
    }
    #endregion



    #region private
    private void animationOverCalled() {
        animationOver = true;
    }
    #endregion
}
