using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveMortgageState")]
internal class ResolveMortgageState : State {
    private bool allMortgagesResolved;
    private bool goToResolveDebt;
    private PlayerInfo unresolvedPlayer;
    private PropertyInfo unresolvedProperty;



    #region State
    public override void enterState() {
        allMortgagesResolved = false;
        goToResolveDebt = false;

        ScreenOverlayEventHub.Instance.sub_KeepMortgageClicked(keepClickedListener);
        ScreenOverlayEventHub.Instance.sub_UnmortgageClicked(unmortgageClickedListener);



        unresolvedPlayer = getPlayerWithUnresolvedMortgage();
        if (unresolvedPlayer == null) {
            allMortgagesResolved = true;
        }
        else {
            unresolvedProperty = unresolvedPlayer.UnresolvedMortgageProperty;
            ScreenOverlayEventHub.Instance.call_ResolveMortgage(unresolvedPlayer, unresolvedProperty);
        }
    }
    public override bool exitConditionMet() {
        return allMortgagesResolved
            || goToResolveDebt;
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_KeepMortgageClicked(keepClickedListener);
        ScreenOverlayEventHub.Instance.unsub_UnmortgageClicked(unmortgageClickedListener);
        unresolvedPlayer = null;
        unresolvedProperty = null;
    }
    public override State getNextState() {
        if (allMortgagesResolved) return allStates.getState<PrerollState>();
        if (goToResolveDebt) return allStates.getState<ResolveDebtState>();
        throw new System.Exception();
    }
    #endregion



    #region private
    private void keepClickedListener() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenOverlay();
        DataEventHub.Instance.call_MortgageIsResolved(unresolvedPlayer, unresolvedProperty);
        DataEventHub.Instance.call_PlayerIncurredDebt(unresolvedPlayer, GameState.game.BankCreditor, unresolvedProperty.RetainMortgageCost);
        goToResolveDebt = true;
    }
    private void unmortgageClickedListener() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenOverlay();
        DataEventHub.Instance.call_MortgageIsResolved(unresolvedPlayer, unresolvedProperty);
        DataEventHub.Instance.call_PropertyUnmortgaged(unresolvedProperty);
        DataEventHub.Instance.call_PlayerIncurredDebt(unresolvedPlayer, GameState.game.BankCreditor, unresolvedProperty.UnmortgageCost);
        UIEventHub.Instance.call_PlayerPropertyAdjustment(unresolvedPlayer, unresolvedProperty);
        WaitFrames.Instance.beforeAction(
            FrameConstants.PLAYER_PANEL_ICON_POP,
            () => goToResolveDebt = true
        );
    }
    private PlayerInfo getPlayerWithUnresolvedMortgage() {
        PlayerInfo playerInfo = null;
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        foreach (PlayerInfo thisPlayer in activePlayers) {
            if (thisPlayer.HasAnUnresolvedMortgage) {
                playerInfo = thisPlayer;
                break;
            }
        }
        return playerInfo;
    }
    #endregion
}
