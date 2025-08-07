using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenState")]
internal class MoveTokenState : State {
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        int startingIndex = GameState.game.SpaceIndexOfTurnPlayer;
        int diceValues = GameState.game.DiceInfo.TotalValue;
        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedAlongBoard(startingIndex, diceValues);
        tokenSettled = false;
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
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
