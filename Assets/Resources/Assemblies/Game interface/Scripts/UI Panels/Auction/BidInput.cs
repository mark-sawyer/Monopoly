using TMPro;
using UnityEngine;

public class BidInput : MonoBehaviour {
    [SerializeField] private PercentageBar percentageBar;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private BidButton bidButton;
    [SerializeField] private BidDisplayText bidDisplayText;
    [SerializeField] private GameObject bidInputGameObject;
    [SerializeField] private GameObject bidDisplayGameObject;
    private PlayerInfo playerInfo;
    private int totalMoney;
    private bool inInputMode;



    #region MonoBehaviour
    private void Start() {
        inputField.onValidateInput += discardNonDigits;
        inputField.onValueChanged.AddListener(reactToNewInputBid);
        AuctionEventHub.Instance.sub_BidMade(reactToNewBidMade);
        inInputMode = true;
    }
    private void OnDestroy() {
        AuctionEventHub.Instance.unsub_BidMade(reactToNewBidMade);
    }
    #endregion



    #region public
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        totalMoney = playerInfo.Money;
        percentageBar.setup(totalMoney);
    }
    public int getEnteredBid() {
        string text = inputField.text;
        if (text == "") return 0;
        else if (!int.TryParse(text, out int bid)) throw new System.Exception();
        else return bid;
    }
    #endregion



    #region private
    private char discardNonDigits(string text, int charIndex, char addedChar) {
        if (!char.IsDigit(addedChar)) return '\0';
        if (text == "" && addedChar == '0') return '\0';
        return addedChar;
    }
    private void reactToNewInputBid(string text) {
        void clampBid() {
            inputField.onValueChanged.RemoveListener(reactToNewInputBid);
            inputField.text = totalMoney.ToString();
            inputField.onValueChanged.AddListener(reactToNewInputBid);            
        }
        string inputWithoutLeadingZeros(string currentInput) {
            string newInput = "";
            int stringLen = currentInput.Length;
            for (int i = 0; i < stringLen; i++) {
                char c = currentInput[i];
                if (c != '0') {
                    newInput = currentInput[i..stringLen];
                    break;
                }
            }
            return newInput;
        }
        void removeLeadingZeros() {
            string currentInput = inputField.text;
            string newInput = inputWithoutLeadingZeros(currentInput);
            if (newInput != currentInput) {
                inputField.onValueChanged.RemoveListener(reactToNewInputBid);
                inputField.text = newInput;
                inputField.onValueChanged.AddListener(reactToNewInputBid);
            }
        }

        int bid = getEnteredBid();
        if (bid > totalMoney) {
            bid = totalMoney;
            clampBid();
        }
        else removeLeadingZeros();
        percentageBar.adjustVisual(bid);
        bidButton.adjustInteractability(bid);
    }
    private void reactToNewBidMade(PlayerInfo biddingPlayer, int bid) {
        void adjustInput() {
            bool currentlyOff = !bidInputGameObject.activeSelf;
            if (currentlyOff) {
                bidInputGameObject.SetActive(true);
                bidDisplayGameObject.SetActive(false);
                inputField.text = "";
            }
            else {
                int enteredBid = getEnteredBid();
                bidButton.adjustInteractability(enteredBid);
            }
        }
        void turnOnBidDisplay() {
            bidInputGameObject.SetActive(false);
            bidDisplayGameObject.SetActive(true);
            bidDisplayText.adjustBidDisplay(bid);
        }


        inInputMode = biddingPlayer != playerInfo;
        if (inInputMode) adjustInput();
        else turnOnBidDisplay();
    }
    #endregion
}
