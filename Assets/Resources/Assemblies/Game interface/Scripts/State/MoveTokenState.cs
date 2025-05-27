using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenState")]
public class MoveTokenState : State {
    [SerializeField] private GameEvent tokenSettledEvent;
    private SpaceVisualManager spacesVisualManager;
    private TokenVisualManager tokenVisualManager;
    private TokenVisual tokenVisual;
    private bool tokenSettled;
    private State ResolveTurnState { get => getStateType<ResolveTurnState>(); }
    private State BuyPropertyOptionState { get => getStateType<BuyPropertyOptionState>(); }



    #region GameState
    public override void enterState() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        tokenVisual = tokenVisualManager.getTokenVisual(turnIndex);
        tokenVisual.beginMovingToNewSpace();
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
        SpaceInfo spaceInfo = GameState.game.SpaceInfoOfTurnPlayer;

        if (spaceInfo is TaxSpaceInfo taxSpaceInfo && taxSpaceInfo.TaxSpaceType == TaxSpaceType.INCOME_TAX) {
            return getStateType<IncomeTaxState>();
        }


        PropertySpaceInfo propertySpaceInfo = spaceInfo as PropertySpaceInfo;
        if (propertySpaceInfo == null) return ResolveTurnState;

        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        if (propertyInfo.IsBought) return ResolveTurnState;

        return BuyPropertyOptionState;
    }
    #endregion



    #region public
    public void setup(SpaceVisualManager spacesVisualManager, TokenVisualManager tokenVisualManager) {
        this.spacesVisualManager = spacesVisualManager;
        this.tokenVisualManager = tokenVisualManager;
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
