using UnityEngine;
using UnityEngine.UI;

public class AuctionPlayerSection : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private BidInput bidInput;
    [SerializeField] private BidButton bidButton;
    [SerializeField] private GameObject highlightPanel;
    private PlayerInfo playerInfo;



    #region MonoBehaviour
    private void Start() {
        AuctionEventHub.Instance.sub_BidMade(highlightToggle);
    }
    private void OnDestroy() {
        AuctionEventHub.Instance.unsub_BidMade(highlightToggle);
    }
    #endregion



    #region public
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
        moneyAdjuster.adjustMoneyQuietly(playerInfo);
        bidInput.setup(playerInfo);
        bidButton.setup(playerInfo);
        setupHighlight();
    }
    #endregion



    #region private
    private void highlightToggle(PlayerInfo biddingPlayer, int bid) {
        bool highlightStatus = playerInfo == biddingPlayer;
        highlightPanel.SetActive(highlightStatus);
    }
    private void setupHighlight() {
        TokenDictionary tokenDictionary = TokenDictionary.Instance;
        PlayerColour playerColour = playerInfo.Colour;
        TokenColours tokenColours = tokenDictionary.getColours(playerColour);
        Color highlightColour = tokenColours.OuterCircleColour;
        Transform sectionsTransform = highlightPanel.transform.GetChild(0);
        for (int i = 0; i < sectionsTransform.childCount; i++) {
            Transform child = sectionsTransform.GetChild(i);
            Image image = child.GetComponent<Image>();
            image.color = highlightColour;
        }
    }
    #endregion
}
