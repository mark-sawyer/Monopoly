using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenState")]
public class MoveTokenState : State {
    [SerializeField] private GameEvent turnPlayerSpaceUpdate;
    [SerializeField] private GameEvent tokenSettledEvent;
    private TokenVisual tokenVisual;
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        tokenVisual = TokenVisualManager.Instance.getTokenVisual(turnIndex);
        int startingIndex = GameState.game.SpaceIndexOfTurnPlayer;
        turnPlayerSpaceUpdate.invoke();
        tokenVisual.beginMovingToNewSpace(startingIndex);
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

        if (spaceInfo is IncomeTaxSpaceInfo) return getState<IncomeTaxState>();
        if (spaceInfo is GoToJailSpaceInfo) return getState<PoliceAnimationState>();

        PropertySpaceInfo propertySpaceInfo = spaceInfo as PropertySpaceInfo;
        if (propertySpaceInfo == null) return getState<ResolveTurnState>();

        PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
        if (propertyInfo.IsBought) return getState<ResolveTurnState>();

        return getState<BuyPropertyOptionState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
