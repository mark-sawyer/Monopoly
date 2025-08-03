using UnityEngine;

public class BidInput : MoneyInput {
    [SerializeField] private BidButton bidButton;
    [SerializeField] private BidDisplayText bidDisplayText;
    [SerializeField] private GameObject bidDisplayGameObject;
    private bool inInputMode;



    #region MonoBehaviour
    private void OnDestroy() {
        AuctionEventHub.Instance.unsub_BidMade(reactToNewBidMade);
    }
    #endregion



    #region public
    public override void setup(PlayerInfo playerInfo) {
        base.setup(playerInfo);
        AuctionEventHub.Instance.sub_BidMade(reactToNewBidMade);
        inInputMode = true;
    }
    #endregion



    #region protected
    protected override void postValueEnteredReaction(int bid) {
        bidButton.adjustInteractability(bid);
    }
    #endregion



    #region private
    private void reactToNewBidMade(PlayerInfo biddingPlayer, int bid) {
        void adjustInput() {
            bool currentlyOff = !MoneyInputGameObject.activeSelf;
            if (currentlyOff) {
                MoneyInputGameObject.SetActive(true);
                bidDisplayGameObject.SetActive(false);
                InputField.text = "";
            }
            else {
                int enteredBid = getEnteredInput();
                bidButton.adjustInteractability(enteredBid);
            }
        }
        void turnOnBidDisplay() {
            MoneyInputGameObject.SetActive(false);
            bidDisplayGameObject.SetActive(true);
            bidDisplayText.adjustBidDisplay(bid);
        }


        inInputMode = biddingPlayer != PlayerInfo;
        if (inInputMode) adjustInput();
        else turnOnBidDisplay();
    }
    #endregion
}
