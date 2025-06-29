using UnityEngine;

[CreateAssetMenu(menuName = "State/RollAnimationState")]
public class RollAnimationState : State {
    [SerializeField] private GameEvent diceAnimationOver;
    private bool animationOver;



    #region GameState
    public override void enterState() {
        diceAnimationOver.Listeners += animationOverCalled;
        animationOver = false;
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenScaler turnTokenScaler = TokenVisualManager.Instance.getTokenScaler(turnIndex);
        TokenVisual turnTokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        turnTokenScaler.beginScaleChange(InterfaceConstants.SCALE_FOR_MOVING_TOKEN);
        turnTokenVisual.changeLayer(InterfaceConstants.MOVING_TOKEN_LAYER_NAME);
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        diceAnimationOver.Listeners -= animationOverCalled;
    }
    public override State getNextState() {
        if (GameState.game.DiceInfo.ThreeDoublesInARow) return allStates.getState<PoliceAnimationState>();
        else return allStates.getState<MoveTokenState>();
    }
    #endregion



    #region private
    private void animationOverCalled() {
        animationOver = true;
    }
    #endregion
}
