using UnityEngine;

[CreateAssetMenu(menuName = "State/GetOutOfJailFreeState")]
internal class GetOutOfJailFreeState : State {
    private bool cardResolvedInvoked;



    #region State
    public override void enterState() {
        cardResolvedInvoked = false;
        CardInfo cardInfo = GameState.game.DrawnCard;
        WaitFrames.Instance.beforeAction(
            50,
            () => {
                DataUIPipelineEventHub.Instance.call_PlayerGetsGOOJFCard(GameState.game.TurnPlayer, cardInfo);
                DataEventHub.Instance.call_CardResolved();
                cardResolvedInvoked = true;
            }
        );
    }
    public override bool exitConditionMet() {
        return cardResolvedInvoked;
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
    #endregion
}
