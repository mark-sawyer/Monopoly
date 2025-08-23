using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumberSelection : ScreenOverlay {
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    private int playerNumber = 4;



    #region ScreenOverlay
    public override void appear() {
        SoundPlayer.Instance.play_QuestionChime();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        StartCoroutine(screenOverlayDropper.drop());
    }
    #endregion



    #region public
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
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        ScreenOverlayStarterEventHub.Instance.call_PlayerNumberConfirmed(playerNumber);
    }
    #endregion
}
