using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenState")]
public class MoveTokenState : State {
    [SerializeField] private GameEvent turnPlayerSpaceUpdate;
    [SerializeField] private GameEvent tokenSettledEvent;
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        int turnIndex = GameState.game.IndexOfTurnPlayer;
        TokenMover tokenMover = TokenVisualManager.Instance.getTokenMover(turnIndex);
        int startingIndex = GameState.game.SpaceIndexOfTurnPlayer;
        turnPlayerSpaceUpdate.invoke();
        DiceInfo diceInfo = GameState.game.DiceInfo;
        tokenMover.startMoving(startingIndex, diceInfo.TotalValue);
        tokenSettled = false;
        tokenSettledEvent.Listeners += heardTokenSettle;
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        tokenSettledEvent.Listeners -= heardTokenSettle;
    }
    public override State getNextState() {
        SpaceInfo spaceInfo = GameState.game.SpaceInfoOfTurnPlayer;

        if (spaceInfo is IncomeTaxSpaceInfo) return allStates.getState<IncomeTaxState>();
        if (spaceInfo is GoToJailSpaceInfo) return allStates.getState<PoliceAnimationState>();
        if (spaceInfo is CardSpaceInfo) return allStates.getState<DrawCardState>();
        if (spaceInfo is PropertySpaceInfo propertySpaceInfo) {
            PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
            if (!propertyInfo.IsBought) return allStates.getState<BuyPropertyOptionState>();
            else if (propertyInfo.Owner != GameState.game.TurnPlayer) return allStates.getState<PayRentState>();
        }
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
