using UnityEngine;

[CreateAssetMenu(menuName = "State/AdvanceToState")]
public class AdvanceToState : State {
    private bool tokenSettled;



    #region State
    public override void enterState() {
        tokenSettled = false;

        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        AdvanceToCardInfo advanceToCardInfo = (AdvanceToCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        SpaceInfo newSpace = advanceToCardInfo.Destination;

        int oldSpaceIndex = turnPlayer.SpaceIndex;
        int newSpaceIndex = newSpace.Index;
        int spacesMoved = Modulus.mod(newSpaceIndex - oldSpaceIndex, GameConstants.TOTAL_SPACES);
        DataEventHub.Instance.call_TurnPlayerMovedAlongBoard(oldSpaceIndex, spacesMoved);
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
