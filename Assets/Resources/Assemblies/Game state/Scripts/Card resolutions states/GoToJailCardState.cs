using UnityEngine;

[CreateAssetMenu(menuName = "State/GoToJailCardState")]
internal class GoToJailCardState : State {
    private bool tokenSettled;



    #region State
    public override void enterState() {
        tokenSettled = false;
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);

        int startingIndex = GameState.game.TurnPlayer.SpaceIndex;
        DataUIPipelineEventHub.Instance.call_TurnPlayerSentToJail(startingIndex);

        DataEventHub.Instance.call_TurnPlayerWillLoseTurn();
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
