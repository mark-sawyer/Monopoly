using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveTurnState")]
internal class ResolveDebtState : State {
    private bool debtResolved;
    private bool playerBankrupt;
    private bool goToRaiseMoney;



    #region State
    public override void enterState() {
        debtResolved = true;
        goToRaiseMoney = false;
        playerBankrupt = false;

        IEnumerable<PlayerInfo> playerInfos = GameState.game.PlayerInfos;
        foreach (PlayerInfo playerInfo in playerInfos) {
            DebtInfo debtInfo = playerInfo.DebtInfo;
            if (debtInfo != null) {
                debtResolved = false;
                resolveDebt(playerInfo, debtInfo);
                break;
            }
        }
    }
    public override bool exitConditionMet() {
        return debtResolved || goToRaiseMoney || playerBankrupt;
    }
    public override State getNextState() {
        if (debtResolved) return allStates.getState<UpdateTurnPlayerState>();
        if (goToRaiseMoney) return allStates.getState<RaiseMoneyState>();
        if (playerBankrupt) return allStates.getState<EliminatePlayerState>();

        throw new System.Exception();
    }
    #endregion



    #region private
    private void resolveDebt(PlayerInfo debtor, DebtInfo debtInfo) {
        void handleDebtRemaining(int paid) {
            if (debtor.CanRaiseMoney && paid > 0) {
                WaitFrames.Instance.beforeAction(
                    FrameConstants.MONEY_UPDATE + 50,
                    () => goToRaiseMoney = true
                );
            }
            else if (debtor.CanRaiseMoney) {
                goToRaiseMoney = true;
            }
            else if (paid > 0) {
                WaitFrames.Instance.beforeAction(
                    FrameConstants.MONEY_UPDATE + 50,
                    () => playerBankrupt = true
                );
            }
            else {
                playerBankrupt = true;
            }
        }


        int money = debtor.Money;
        int owed = debtInfo.Owed;
        int paid = money - owed >= 0 ? owed : money;
        if (paid > 0) DataUIPipelineEventHub.Instance.call_DebtReduced(debtor, paid);
        if (debtInfo.Owed == 0) {
            WaitFrames.Instance.beforeAction(
                FrameConstants.MONEY_UPDATE,
                () => debtResolved = true
            );
        }
        else handleDebtRemaining(paid);
    }
    #endregion
}
