using UnityEngine;

[CreateAssetMenu(menuName = "State/MoneyCardState")]
internal class MoneyCardState : State {
    private int addedToPlayer;
    private bool goToNextState;



    #region State
    public override void enterState() {
        goToNextState = false;
        MoneyDifferenceCardInfo moneyDifferenceCardInfo = (MoneyDifferenceCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        addedToPlayer = moneyDifferenceCardInfo.AddedToPlayer;
        if (addedToPlayer > 0) positiveCard();
        else negativeCard();
        DataEventHub.Instance.call_CardResolved();
    }
    public override bool exitConditionMet() {
        return goToNextState;
    }
    public override State getNextState() {
        if (addedToPlayer > 0) return allStates.getState<UpdateTurnPlayerState>();
        else return allStates.getState<ResolveDebtState>();
    }
    #endregion




    #region
    private void positiveCard() {
        DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, addedToPlayer);
        WaitFrames.Instance.beforeAction(
            FrameConstants.MONEY_UPDATE,
            () => goToNextState = true
        );
    }
    private void negativeCard() {
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, GameState.game.BankCreditor, -addedToPlayer);
        if (GameState.game.TurnPlayer.Money > 0) {
            goToNextState = true;
        }
        else {
            WaitFrames.Instance.beforeAction(
                50,
                () => goToNextState = true
            );
        }
    }
    #endregion
}
