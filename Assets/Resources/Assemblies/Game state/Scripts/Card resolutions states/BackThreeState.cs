using UnityEngine;

[CreateAssetMenu(menuName = "State/BackThreeState")]
internal class BackThreeState : State {
    private bool tokenSettled;



    #region State
    public override void enterState() {
        tokenSettled = false;
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int newSpaceIndex = (oldSpaceIndex - 3).mod(GameConstants.TOTAL_SPACES);
        SpaceInfo newSpace = SpaceVisualManager.Instance.getSpaceVisual(newSpaceIndex).SpaceInfo;
        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedToSpace(newSpace, oldSpaceIndex);
        DataEventHub.Instance.call_CardResolved();
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
