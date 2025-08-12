using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/ResolveMortgageState")]
internal class ResolveMortgageState : State {
    private bool mortgageResolved;
    private PlayerInfo playerInfo;
    private PropertyInfo unresolvedMortgageProperty;



    #region State
    public override void enterState() {
        mortgageResolved = false;
        ScreenOverlayEventHub.Instance.sub_KeepMortgageClicked(keepClickedListener);
        ScreenOverlayEventHub.Instance.sub_UnmortgageClicked(unmortgageClickedListener);

        playerInfo = getPlayerWithUnresolvedMortgage();
        unresolvedMortgageProperty = playerInfo.UnresolvedMortgageProperty;
        ScreenOverlayEventHub.Instance.call_ResolveMortgage(playerInfo, unresolvedMortgageProperty);
    }
    public override bool exitConditionMet() {
        return mortgageResolved;
    }
    public override void exitState() {
        ScreenOverlayEventHub.Instance.unsub_KeepMortgageClicked(keepClickedListener);
        ScreenOverlayEventHub.Instance.unsub_UnmortgageClicked(unmortgageClickedListener);

        playerInfo = null;
        unresolvedMortgageProperty = null;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    #region private
    private void keepClickedListener() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenAnimation();
        DataEventHub.Instance.call_MortgageIsResolved(playerInfo, unresolvedMortgageProperty);
        DataEventHub.Instance.call_PlayerIncurredDebt(playerInfo, GameState.game.BankCreditor, unresolvedMortgageProperty.RetainMortgageCost);
        mortgageResolved = true;
    }
    private void unmortgageClickedListener() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenAnimation();
        DataEventHub.Instance.call_MortgageIsResolved(playerInfo, unresolvedMortgageProperty);
        DataEventHub.Instance.call_PropertyUnmortgaged(unresolvedMortgageProperty);
        DataEventHub.Instance.call_PlayerIncurredDebt(playerInfo, GameState.game.BankCreditor, unresolvedMortgageProperty.UnmortgageCost);
        UIEventHub.Instance.call_PlayerPropertyAdjustment(playerInfo, unresolvedMortgageProperty);
        WaitFrames.Instance.beforeAction(
            FrameConstants.PLAYER_PANEL_ICON_POP,
            () => mortgageResolved = true
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
