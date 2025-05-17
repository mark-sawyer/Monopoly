using UnityEngine;

public class MoveTokenState : State {
    private GamePlayer gamePlayer;
    private SpaceVisualManager spacesVisualManager;
    private TokenVisualManager tokenVisualManager;
    private TokenVisual tokenVisual;
    private bool tokenSettled;
    private State ResolveTurnState { get => getStateType<ResolveTurnState>(); }
    private State BuyPropertyOptionState { get => getStateType<BuyPropertyOptionState>(); }



    #region GameState
    public override void enterState() {
        gamePlayer.moveTurnPlayerDiceValues();
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        tokenVisual = tokenVisualManager.getTokenVisual(turnIndex);
        tokenVisual.beginMovingToNewSpace();
        tokenSettled = false;
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenVisual.changeLayer(InterfaceConstants.BOARD_TOKEN_LAYER_NAME);
    }
    public override State getNextState() {
        SpaceInfo spaceInfo = GameState.game.SpaceInfoOfTurnPlayer;

        PropertySpaceInfo propertySpaceInfo = spaceInfo as PropertySpaceInfo;
        if (propertySpaceInfo == null) return ResolveTurnState;

        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        if (propertyInfo.IsBought) return ResolveTurnState;

        return BuyPropertyOptionState;
    }
    #endregion



    #region public
    public void setup(SpaceVisualManager spacesVisualManager, TokenVisualManager tokenVisualManager, GamePlayer gamePlayer) {
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
