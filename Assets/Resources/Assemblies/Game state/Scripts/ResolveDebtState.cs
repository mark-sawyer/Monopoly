using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveTurnState")]
internal class ResolveDebtState : State {
    private bool debtResolved;
    private bool playerBankrupt;



    #region State
    public override void enterState() {
        debtResolved = true;
        playerBankrupt = false;


        IEnumerable<PlayerInfo> playerInfos = GameState.game.PlayerInfos;
        foreach (PlayerInfo playerInfo in playerInfos) {
            DebtInfo debtInfo = playerInfo.Debt;
            if (debtInfo != null) {
                debtResolved = false;
                resolveDebt(playerInfo, debtInfo);
                break;
            }
        }
    }
    public override bool exitConditionMet() {
        return debtResolved || playerBankrupt;
    }
    public override State getNextState() {
        if (debtResolved) return allStates.getState<UpdateTurnPlayerState>();
        else return allStates.getState<EliminatePlayerState>();
    }
    #endregion



    #region private
    private void resolveDebt(PlayerInfo debtor, DebtInfo debtInfo) {
        void handleDebtRemaining(int paid) {
            if (debtor.CanRaiseMoney && paid > 0) {
                WaitFrames.Instance.beforeAction(
                    InterfaceConstants.FRAMES_FOR_MONEY_UPDATE + 50,
                    () => ScreenAnimationEventHub.Instance.call_ResolveDebt(debtInfo)
                );
            }
            else if (debtor.CanRaiseMoney) {
                ScreenAnimationEventHub.Instance.call_ResolveDebt(debtInfo);
            }
            else if (paid > 0) {
                WaitFrames.Instance.beforeAction(
                    InterfaceConstants.FRAMES_FOR_MONEY_UPDATE + 50,
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
        if (debtInfo.Owed == 0) debtResolved = true;
        else handleDebtRemaining(paid);
    }
    #endregion
}
