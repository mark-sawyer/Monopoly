using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/AuctionPropertyState")]
internal class AuctionPropertyState : State {
    private bool auctionOver;



    #region State
    public override void enterState() {
        auctionOver = false;

        Queue<TradableInfo> tradableInfos = new();
        PropertyInfo propertyLandedOn = ((PropertySpaceInfo)GameState.game.TurnPlayer.SpaceInfo).PropertyInfo;
        tradableInfos.Enqueue(propertyLandedOn);
        ScreenOverlayEventHub.Instance.call_AuctionsBegin(tradableInfos);

        AuctionEventHub.Instance.sub_AllAuctionsFinished(auctionOverListening);
    }
    public override bool exitConditionMet() {
        return auctionOver;
    }
    public override void exitState() {
        AuctionEventHub.Instance.unsub_AllAuctionsFinished(auctionOverListening);
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    #region private
    private void auctionOverListening() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenAnimationKeepCover();
        auctionOver = true;
    }
    #endregion
}
