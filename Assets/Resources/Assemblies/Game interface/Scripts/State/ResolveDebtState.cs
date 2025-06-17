using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveTurnState")]
public class ResolveDebtState : State {
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerEvent debtResolvedEvent;
    [SerializeField] private SoundEvent moneyChing;
    private bool debtResolved;
    private PlayerInfo debtor;
    private DebtInfo debt;

    public override void enterState() {
        debtor = null;
        debt = null;
        debtResolved = true;

        IEnumerable<PlayerInfo> playerInfos = GameState.game.PlayerInfos;
        foreach (PlayerInfo playerInfo in playerInfos) {
            DebtInfo debtInfo = playerInfo.Debt;
            if (debtInfo != null) {
                debtResolved = false;
                debtor = playerInfo;
                debt = debtInfo;
                break;
            }
        }
    }
    public override void update() {
        if (debtResolved) return;

        int money = debtor.Money;
        int owed = debt.Owed;
        int payable = money - owed >= 0 ? owed : money;
        moneyAdjustment.invoke(debtor, -payable);
        if (debt.Creditor is PlayerInfo creditorPlayer) moneyAdjustment.invoke(creditorPlayer, payable);
        moneyChing.play();
        debtResolvedEvent.invoke(debt.Debtor);

        debtResolved = true;
    }
    public override bool exitConditionMet() {
        return debtResolved;
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
}
