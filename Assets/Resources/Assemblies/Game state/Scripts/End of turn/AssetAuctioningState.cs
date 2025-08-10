using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/AssetAuctioningState")]
internal class AssetAuctioningState : State {
    private bool auctionsOver;



    #region State
    public override void enterState() {
        auctionsOver = false;
        AuctionEventHub.Instance.sub_AllAuctionsFinished(auctionsOverListening);

        IEnumerable<TradableInfo> tradableInfos = GameState.game.TurnPlayer.TradableInfos;
        Queue<TradableInfo> tradablesQueue = new Queue<TradableInfo>(tradableInfos);
        ScreenOverlayEventHub.Instance.call_AuctionsBegin(tradablesQueue);
    }
    public override bool exitConditionMet() {
        return auctionsOver;
    }
    public override void exitState() {
        AuctionEventHub.Instance.unsub_AllAuctionsFinished(auctionsOverListening);
    }
    public override State getNextState() {
        return allStates.getState<UpdateTurnPlayerState>();
    }
    #endregion



    #region private
    private void auctionsOverListening() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenAnimationKeepCover();
        auctionsOver = true;
    }
    #endregion
}
