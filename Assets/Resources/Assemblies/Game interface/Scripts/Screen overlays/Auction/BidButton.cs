using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BidButton : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private MoneyInput bidInput;
    private PlayerInfo playerInfo;



    #region MonoBehaviour
    private void Start() {
        button.interactable = false;
        AuctionEventHub.Instance.sub_BidMade(reactToBid);
    }
    private void OnDestroy() {
        AuctionEventHub.Instance.unsub_BidMade(reactToBid);
    }
    #endregion



    #region public
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
    }
    public void bidClicked() {
        int bid = bidInput.getEnteredInput();
        acceptBid(bid);
        AuctionEventHub.Instance.call_BidMade(playerInfo, bid);
    }
    public void adjustInteractability(int textInputBid) {
        int minimumBid = getCorrectBid() + 1;
        button.interactable = textInputBid >= minimumBid;
    }
    #endregion



    #region private
    private void reactToBid(PlayerInfo biddingPlayer, int bid) {
        if (biddingPlayer == playerInfo) {
            button.interactable = false;
            return;
        }
    }
    private void acceptBid(int bid) {
        if (AuctionManager<Queue<PropertyInfo>>.Instance != null) {
            AuctionManager<Queue<PropertyInfo>>.Instance.acceptNewBid(bid, playerInfo);
        }
        else {
            AuctionManager<BuildingType>.Instance.acceptNewBid(bid, playerInfo);
        }
    }
    private int getCorrectBid() {
        if (AuctionManager<Queue<PropertyInfo>>.Instance != null) {
            return AuctionManager<Queue<PropertyInfo>>.Instance.CurrentBid;
        }
        else {
            return AuctionManager<BuildingType>.Instance.CurrentBid;
        }
    }
    #endregion
}
