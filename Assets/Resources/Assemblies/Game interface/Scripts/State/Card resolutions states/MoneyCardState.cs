using UnityEngine;

[CreateAssetMenu(menuName = "State/MoneyCardState")]
public class MoneyCardState : State {
    [SerializeField] private SoundEvent moneyChing;
    private int addedToPlayer;



    #region State
    public override void enterState() {
        MoneyDifferenceCardInfo moneyDifferenceCardInfo = (MoneyDifferenceCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        addedToPlayer = moneyDifferenceCardInfo.AddedToPlayer;
        if (addedToPlayer > 0) {
            DataEventHub.Instance.call_MoneyAdjustment(GameState.game.TurnPlayer, addedToPlayer);
            moneyChing.play();
        }
        else DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, GameState.game.Bank, -addedToPlayer);
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
