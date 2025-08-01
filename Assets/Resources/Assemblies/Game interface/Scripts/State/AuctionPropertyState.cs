using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "State/AuctionPropertyState")]
public class AuctionPropertyState : State {
    private bool auctionOver;



    #region State
    public override void enterState() {
        auctionOver = false;
        AuctionEventHub.Instance.sub_WinnerAnnounced(auctionOverListening);

        UIEventHub.Instance.call_FadeScreenCoverIn(1f);
        List<PlayerInfo> activePlayers = GameState.game.ActivePlayers.ToList();
        AuctionManager.Instance.appear(activePlayers);
    }
    public override bool exitConditionMet() {
        return auctionOver;
    }
    public override void exitState() {
        AuctionEventHub.Instance.unsub_WinnerAnnounced(auctionOverListening);
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    #region private
    private void auctionOverListening(PlayerInfo winningPlayer, int bid) {
        if (winningPlayer == null) {
            auctionOver = true;
            return;
        }

        DataEventHub.Instance.call_MoneyAdjustment(winningPlayer, -bid);
        PropertyInfo propertyInfo = ((PropertySpaceInfo)GameState.game.TurnPlayer.SpaceInfo).PropertyInfo;
        WaitFrames.Instance.exe(
            90,
            () => {
                DataEventHub.Instance.call_PlayerObtainedProperty(winningPlayer, propertyInfo);
                auctionOver = true;
            }
        );
    }
    #endregion
}
