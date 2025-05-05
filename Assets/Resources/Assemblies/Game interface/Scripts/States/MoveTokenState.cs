
public class MoveTokenState : State {
    private GamePlayer gamePlayer;
    private SpaceVisualManager spacesVisualManager;
    private TokenVisualManager tokenVisualManager;
    private TokenVisual tokenVisual;
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        gamePlayer.moveTurnPlayerDiceValues();
        tokenVisual = tokenVisualManager.getTurnTokenVisual();
        tokenVisual.beginMovingToNewSpace();
        tokenSettled = false;
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenVisual.changeLayer(false);
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
        GameEvents.tokenSettled.AddListener(heardTokenSettle);
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
