using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerMoneyCardState")]
internal class PlayerMoneyCardState : State {
    #region State
    public override void enterState() {
        PlayerMoneyDifferenceCardInfo pmdCardInfo = (PlayerMoneyDifferenceCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        int subtracted = pmdCardInfo.SubtractedFromOtherPlayers;
        if (subtracted > 0) othersGiveMoney(subtracted);
        else { }
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    #region private
    private void othersGiveMoney(int given) {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        foreach (PlayerInfo activePlayer in activePlayers) {
            if (activePlayer == turnPlayer) continue;

            DataEventHub.Instance.call_PlayerIncurredDebt(activePlayer, turnPlayer, given);
        }
    }
    private void turnPlayerGivesMoney() { }
    #endregion
}
