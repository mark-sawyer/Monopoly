using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumberSelection : DroppedQuestion {
    [SerializeField] private GameEvent<int> tokenSelectionQuestion;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    private int playerNumber = 4;


    public void increase() {
        playerNumber++;
        numberText.text = playerNumber.ToString();
        if (playerNumber == 8) increaseButton.interactable = false;
        if (!decreaseButton.interactable) decreaseButton.interactable = true;
    }
    public void decrease() {
        playerNumber--;
        numberText.text = playerNumber.ToString();
        if (playerNumber == 2) decreaseButton.interactable = false;
        if (!increaseButton.interactable) increaseButton.interactable = true;
    }
    public void confirm() {
        tokenSelectionQuestion.invoke(playerNumber);
        questionAnswered.invoke();
        disappear();
    }
}
