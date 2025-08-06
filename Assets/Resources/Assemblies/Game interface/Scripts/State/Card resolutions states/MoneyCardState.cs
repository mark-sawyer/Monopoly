using UnityEngine;

[CreateAssetMenu(menuName = "State/MoneyCardState")]
public class MoneyCardState : State {
    private int addedToPlayer;



    #region State
    public override void enterState() {
        MoneyDifferenceCardInfo moneyDifferenceCardInfo = (MoneyDifferenceCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        addedToPlayer = moneyDifferenceCardInfo.AddedToPlayer;
        if (addedToPlayer > 0) {
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, addedToPlayer);
        }
        else DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, GameState.game.BankCreditor, -addedToPlayer);
        DataEventHub.Instance.call_CardResolved();
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        if (addedToPlayer > 0) return allStates.getState<UpdateTurnPlayerState>();
        else return allStates.getState<ResolveDebtState>();
    }
    #endregion
}
