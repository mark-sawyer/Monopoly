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
        AuctionManager.Instance.acceptNewBid(bid, playerInfo);
        AuctionEventHub.Instance.call_BidMade(playerInfo, bid);
    }
    public void adjustInteractability(int textInputBid) {
        int minimumBid = AuctionManager.Instance.CurrentBid + 1;
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
