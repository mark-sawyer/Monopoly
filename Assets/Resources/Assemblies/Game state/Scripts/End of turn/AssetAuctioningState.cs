using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "State/AssetAuctioningState")]
internal class AssetAuctioningState : State {
    private bool auctionsOver;



    #region State
    public override void enterState() {
        auctionsOver = false;
        AuctionEventHub.Instance.sub_AllAuctionsFinished(auctionsOverListening);

        IEnumerable<TradableInfo> tradableInfos = GameState.game.BankInfo.EliminatedPlayerAssets;
        List<CardInfo> cardInfos = tradableInfos.OfType<CardInfo>().ToList();
        foreach (CardInfo cardInfo in cardInfos) {
            DataEventHub.Instance.call_CardReturned(cardInfo);
        }
        List<PropertyInfo> propertyInfos = tradableInfos.OfType<PropertyInfo>().ToList();
        Queue<PropertyInfo> propertiesQueue = new Queue<PropertyInfo>(propertyInfos);
        ScreenOverlayStarterEventHub.Instance.call_AuctionsBegin(propertiesQueue);
    }
    public override bool exitConditionMet() {
        return auctionsOver;
    }
    public override void exitState() {
        AuctionEventHub.Instance.unsub_AllAuctionsFinished(auctionsOverListening);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion



    #region private
    private void auctionsOverListening() {
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        auctionsOver = true;
    }
    #endregion
}
