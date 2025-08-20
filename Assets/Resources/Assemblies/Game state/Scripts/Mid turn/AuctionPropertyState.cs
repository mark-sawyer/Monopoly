using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/AuctionPropertyState")]
internal class AuctionPropertyState : State {
    private bool auctionOver;



    #region State
    public override void enterState() {
        auctionOver = false;
        AuctionEventHub.Instance.sub_AllAuctionsFinished(auctionOverListening);

        Queue<PropertyInfo> propertyInfos = new();
        PropertyInfo propertyLandedOn = ((PropertySpaceInfo)GameState.game.TurnPlayer.SpaceInfo).PropertyInfo;
        propertyInfos.Enqueue(propertyLandedOn);
        ScreenOverlayEventHub.Instance.call_AuctionsBegin(propertyInfos);
    }
    public override bool exitConditionMet() {
        return auctionOver;
    }
    public override void exitState() {
        AuctionEventHub.Instance.unsub_AllAuctionsFinished(auctionOverListening);
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
    #endregion



    #region private
    private void auctionOverListening() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        auctionOver = true;
    }
    #endregion
}
