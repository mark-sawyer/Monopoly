using UnityEngine;

[CreateAssetMenu(menuName = "State/GetOutOfJailFreeState")]
public class GetOutOfJailFreeState : State {
    private bool cardResolveInvoked;



    #region State
    public override void enterState() {
        cardResolveInvoked = false;
        CardInfo cardInfo = GameState.game.DrawnCard;
        //WaitFrames.Instance.exe(40, pop.play);
        WaitFrames.Instance.exe(50, () => {
            DataEventHub.Instance.call_PlayerGetsGOOJFCard(GameState.game.TurnPlayer, cardInfo);
            DataEventHub.Instance.call_CardResolved();
            cardResolveInvoked = true;
        });
    }
    public override bool exitConditionMet() {
        return cardResolveInvoked;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion
}
