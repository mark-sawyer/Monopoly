using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BidButton : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private MoneyInput bidInput;
    private AuctionEventHub auctionEventHub;
    private AuctionManager auctionManager;
    private PlayerInfo playerInfo;



    #region MonoBehaviour
    private void Start() {
        auctionEventHub = AuctionEventHub.Instance;
        auctionManager = AuctionManager.Instance;
        button.interactable = false;
        auctionEventHub.sub_BidMade(reactToBid);
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
        auctionManager.acceptNewBid(bid, playerInfo);
        auctionEventHub.call_BidMade(playerInfo, bid);
    }
    public void adjustInteractability(int textInputBid) {
        int minimumBid = auctionManager.CurrentBid + 1;
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
    #endregion
}
