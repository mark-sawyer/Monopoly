using System.Collections;
using UnityEngine;

public class IncomeTaxQuestion : ScreenAnimation<PlayerInfo> {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private QuestionCircle questionCircle;
    [SerializeField] private TenPercentButton tenPercentButtonText;
    private PlayerInfo player;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;
    private const int WAITED_FRAMES = 150;



    #region ScreenAnimation
    public override void appear() {
        StartCoroutine(droppingQuestionsFunctionality.drop());
    }
    public override void setup(PlayerInfo player) {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality((RectTransform)transform);
        droppingQuestionsFunctionality.adjustSize();
        this.player = player;
        tokenIcon.setup(player.Token, player.Colour);
    }
    #endregion



    #region public
    public void twoHundredClicked() {
        questionCircle.enabled = false;
        int amount = GameState.game.TurnPlayer.IncomeTaxAmount;
        tenPercentButtonText.updateText(amount);
        if (amount > 200) UIEventHub.Instance.call_CorrectOutcome();
        else UIEventHub.Instance.call_IncorrectOutcome();
        WaitFrames.Instance.exe(WAITED_FRAMES, completeQuestion, 200);
    }
    public void tenPercentClicked() {
        questionCircle.enabled = false;
        int amount = GameState.game.TurnPlayer.IncomeTaxAmount;
        tenPercentButtonText.updateText(amount);
        if (amount <= 200) UIEventHub.Instance.call_CorrectOutcome();
        else UIEventHub.Instance.call_IncorrectOutcome();
        WaitFrames.Instance.exe(WAITED_FRAMES, completeQuestion, player.IncomeTaxAmount);
    }
    #endregion



    #region private
    private void completeQuestion(int moneyLost) {
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, GameState.game.BankCreditor, moneyLost);
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation();
    }
    #endregion
}
