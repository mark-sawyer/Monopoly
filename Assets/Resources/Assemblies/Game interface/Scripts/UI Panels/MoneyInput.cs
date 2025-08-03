using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public abstract class MoneyInput : MonoBehaviour {
    [SerializeField] private PercentageBar percentageBar;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject moneyInputGameObject;
    private PlayerInfo playerInfo;
    private int totalMoney;



    #region public
    public virtual void setup(PlayerInfo playerInfo) {
        inputField.onValidateInput += discardNonDigits;
        inputField.onValueChanged.AddListener(reactToNewInput);
        this.playerInfo = playerInfo;
        totalMoney = playerInfo.Money;
        percentageBar.setup(totalMoney);
    }
    public int getEnteredInput() {
        string text = inputField.text;
        if (text == "") return 0;
        else if (!int.TryParse(text, out int input)) throw new System.Exception();
        else return input;
    }
    public void clearInput() {
        inputField.onValueChanged.RemoveListener(reactToNewInput);
        inputField.text = "";
        inputField.onValueChanged.AddListener(reactToNewInput);
        percentageBar.adjustVisual(0);
    }
    #endregion



    #region protected
    protected PlayerInfo PlayerInfo => playerInfo;
    protected PercentageBar PercentageBar => percentageBar;
    protected TMP_InputField InputField => inputField;
    protected GameObject MoneyInputGameObject => moneyInputGameObject;
    protected virtual void postValueEnteredReaction(int bid) { }
    #endregion



    #region private
    private char discardNonDigits(string text, int charIndex, char addedChar) {
        if (!char.IsDigit(addedChar)) return '\0';
        if (text == "" && addedChar == '0') return '\0';
        return addedChar;
    }
    private void reactToNewInput(string text) {
        void clampBid() {
            inputField.onValueChanged.RemoveListener(reactToNewInput);
            inputField.text = totalMoney.ToString();
            inputField.onValueChanged.AddListener(reactToNewInput);
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
                inputField.onValueChanged.RemoveListener(reactToNewInput);
                inputField.text = newInput;
                inputField.onValueChanged.AddListener(reactToNewInput);
            }
        }

        int input = getEnteredInput();
        if (input > totalMoney) {
            input = totalMoney;
            clampBid();
        }
        else removeLeadingZeros();
        percentageBar.adjustVisual(input);
        postValueEnteredReaction(input);
    }
    #endregion
}