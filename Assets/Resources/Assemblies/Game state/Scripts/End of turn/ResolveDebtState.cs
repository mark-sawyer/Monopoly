using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveDebtState")]
internal class ResolveDebtState : State {
    private bool allDebtsResolved;
    private bool debtsRemaining;
    private bool goToRaiseMoney;



    #region State
    public override void enterState() {
        allDebtsResolved = false;
        debtsRemaining = false;
        goToRaiseMoney = false;


        PlayerInfo playerInDebt = GameState.game.PlayerInDebt;
        if (playerInDebt != null) {
            resolveDebt(playerInDebt, playerInDebt.DebtInfo);
        }
        else {
            allDebtsResolved = true;
        }
    }
    public override bool exitConditionMet() {
        return allDebtsResolved
            || debtsRemaining
            || goToRaiseMoney;
    }
    public override State getNextState() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (allDebtsResolved) {
            if (turnPlayer.ToMoveAfterJailDebtResolving && turnPlayer.IsActive) {
                DataEventHub.Instance.call_SetJailDebtBool(GameState.game.TurnPlayer, false);
                return allStates.getState<MoveTokenState>();
            }
            else return allStates.getState<ResolveMortgageState>();
        }
        if (debtsRemaining) return this;
        if (goToRaiseMoney) return allStates.getState<RaiseMoneyState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void resolveDebt(PlayerInfo debtor, DebtInfo debtInfo) {
        void handleDebtRemaining(int paid) {
            if (paid > 0) {
                WaitFrames.Instance.beforeAction(
                    FrameConstants.MONEY_UPDATE + 50,
                    () => goToRaiseMoney = true
                );
            }
            else {
                goToRaiseMoney = true;
            }
        }
        void confirmWhetherMoreDebts() {
            PlayerInfo nextDebtPlayer = GameState.game.PlayerInDebt;
            if (nextDebtPlayer == null) allDebtsResolved = true;
            else debtsRemaining = true;
        }


        int money = debtor.Money;
        int owed = debtInfo.TotalOwed;
        int paid = money > owed ? owed : money;
        if (paid > 0) {
            if (debtInfo is SingleCreditorDebtInfo) {
                DataUIPipelineEventHub.Instance.call_SingleCreditorDebtReduced(debtor, paid);
            }
            else {
                DataUIPipelineEventHub.Instance.call_MultiCreditorDebtReduced(debtor, paid);
            }
        }
        if (debtInfo.TotalOwed == 0) {
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE,
                confirmWhetherMoreDebts
            );
        }
        else handleDebtRemaining(paid);
    }
    #endregion
}
