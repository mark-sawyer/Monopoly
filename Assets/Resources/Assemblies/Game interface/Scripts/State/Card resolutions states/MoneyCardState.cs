using UnityEngine;

[CreateAssetMenu(menuName = "State/MoneyCardState")]
public class MoneyCardState : State {
    [SerializeField] private GameEvent cardResolved;
    [SerializeField] private SoundEvent moneyChing;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    private int addedToPlayer;



    #region State
    public override void enterState() {
        MoneyDifferenceCardInfo moneyDifferenceCardInfo = (MoneyDifferenceCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        addedToPlayer = moneyDifferenceCardInfo.AddedToPlayer;
        if (addedToPlayer > 0) {
            moneyAdjustment.invoke(GameState.game.TurnPlayer, addedToPlayer);
            moneyChing.play();
        }
        else playerIncurredDebt.invoke(GameState.game.TurnPlayer, GameState.game.Bank, -addedToPlayer);
        cardResolved.invoke();
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
