using UnityEngine;
using UnityEngine.UI;

public class IncomeTaxQuestion : ScreenOverlay<PlayerInfo> {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private QuestionCircle questionCircle;
    [SerializeField] private TenPercentButton tenPercentButtonText;
    [SerializeField] private Button twoHundredButton;
    [SerializeField] private Button tenPercentButton;
    private PlayerInfo player;
    private const int WAITED_FRAMES = 150;



    #region ScreenAnimation
    public override void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player.Token, player.Colour);
    }
    public override void appear() {
        SoundPlayer.Instance.play_QuestionChime();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        StartCoroutine(screenOverlayDropper.drop());
    }
    #endregion



    #region public
    public void twoHundredClicked() {
        disableUI();
        int amount = player.IncomeTaxAmount;
        tenPercentButtonText.updateText(amount);
        if (amount > 200) SoundPlayer.Instance.play_CorrectSound();
        else SoundPlayer.Instance.play_IncorrectSound();
        WaitFrames.Instance.beforeAction(WAITED_FRAMES, completeQuestion, 200);
    }
    public void tenPercentClicked() {
        disableUI();
        int amount = player.IncomeTaxAmount;
        tenPercentButtonText.updateText(amount);
        if (amount <= 200) SoundPlayer.Instance.play_CorrectSound();
        else SoundPlayer.Instance.play_IncorrectSound();
        WaitFrames.Instance.beforeAction(WAITED_FRAMES, completeQuestion, amount);
    }
    #endregion



    #region private
    private void completeQuestion(int moneyLost) {
        DataEventHub.Instance.call_PlayerIncurredDebt(player, GameState.game.BankCreditor, moneyLost);
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
        ScreenOverlayFunctionEventHub.Instance.call_IncomeTaxAnswered();
    }
    private void disableUI() {
        twoHundredButton.interactable = false;
        tenPercentButton.interactable = false;
        questionCircle.enabled = false;
    }
    #endregion
}
