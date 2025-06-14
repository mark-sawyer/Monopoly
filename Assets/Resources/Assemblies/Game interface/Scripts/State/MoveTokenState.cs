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
        return allStates.getState<PlayerLandedOnSpaceState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
