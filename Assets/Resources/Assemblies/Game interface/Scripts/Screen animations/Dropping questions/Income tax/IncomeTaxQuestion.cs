using UnityEngine;

public class IncomeTaxQuestion : DroppedQuestion {
    [SerializeField] private TokenIcon tokenIcon;
    #region GameEvents
    [SerializeField] private GameEvent<PlayerInfo, Creditor, int> playerIncurredDebtEvent;
    [SerializeField] private GameEvent correct;
    [SerializeField] private GameEvent incorrect;
    #endregion
    [SerializeField] private QuestionCircle questionCircle;
    [SerializeField] private TenPercentButton tenPercentButtonText;
    private PlayerInfo player;
    private const int WAITED_FRAMES = 150;



    public void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player.Token, player.Colour);
    }
    public void twoHundredClicked() {
        int amount = GameState.game.TurnPlayer.IncomeTaxAmount;
        tenPercentButtonText.updateText(amount);
        if (amount > 200) correct.invoke();
        else incorrect.invoke();
        WaitFrames.Instance.exe(WAITED_FRAMES, completeQuestion, 200);
    }
    public void tenPercentClicked() {
        int amount = GameState.game.TurnPlayer.IncomeTaxAmount;
        tenPercentButtonText.updateText(amount);
        if (amount <= 200) correct.invoke();
        else incorrect.invoke();
        WaitFrames.Instance.exe(WAITED_FRAMES, completeQuestion, player.IncomeTaxAmount);
    }



    protected override void dropComplete() {
        questionCircle.enabled = true;
    }



    private void completeQuestion(int moneyLost) {
        questionAnswered.invoke();
        playerIncurredDebtEvent.invoke(GameState.game.TurnPlayer, GameState.game.Bank, moneyLost);
        Destroy(gameObject);
    }
}
