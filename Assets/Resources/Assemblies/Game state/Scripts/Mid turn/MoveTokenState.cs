using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenState")]
internal class MoveTokenState : State {
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        tokenSettled = false;
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);


        int startingIndex = GameState.game.TurnPlayer.SpaceIndex;
        int diceValues = GameState.game.DiceInfo.TotalValue;
        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedAlongBoard(startingIndex, diceValues);
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
