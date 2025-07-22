using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BidInput : MonoBehaviour {
    [SerializeField] private PercentageBar percentageBar;
    [SerializeField] private TMP_InputField inputField;
    private int totalMoney;



    #region MonoBehaviour
    private void Start() {
        inputField.onValidateInput += discardNonDigits;
        inputField.onValueChanged.AddListener(clampBid);
    }
    #endregion



    #region public
    public void setup(int totalMoney) {
        this.totalMoney = totalMoney;
        percentageBar.setup(totalMoney);
    }
    #endregion



    #region private
    private char discardNonDigits(string text, int charIndex, char addedChar) {
        if (char.IsDigit(addedChar)) return addedChar;
        else return '\0';
    }
    private void clampBid(string text) {
        if (!int.TryParse(text, out int bid)) return;
        
        if (bid > totalMoney) {
            inputField.onValueChanged.RemoveListener(clampBid);
            inputField.text = totalMoney.ToString();
            inputField.onValueChanged.AddListener(clampBid);
            bid = totalMoney;
        }
        percentageBar.adjustVisual(bid);
    }
    #endregion
}
