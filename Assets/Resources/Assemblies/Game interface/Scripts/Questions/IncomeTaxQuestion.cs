using UnityEngine;

public class IncomeTaxQuestion : MonoBehaviour {
    [SerializeField] private TokenIcon tokenIcon;
    #region GameEvents
    [SerializeField] private GameEvent questionAnswered;
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustmentData;
    [SerializeField] private GameEvent<PlayerInfo> moneyAdjustmentUI;
    [SerializeField] private GameEvent moneyChangedHands;
    #endregion
    [SerializeField] private TenPercentButtonText tenPercentButtonText;
    private PlayerInfo player;
    private const int WAITED_FRAMES = 150;

    public void setup(PlayerInfo player) {
        this.player = player;
        tokenIcon.setup(player.Token, player.Colour);
    }
    public void twoHundredClicked() {
        tenPercentButtonText.updateText();
        StartCoroutine(WaitFrames.exe(WAITED_FRAMES, completeQuestion, 200));
    }
    public void tenPercentClicked() {
        tenPercentButtonText.updateText();
        StartCoroutine(WaitFrames.exe(WAITED_FRAMES, completeQuestion, player.IncomeTaxAmount));
    }

    private void completeQuestion(int moneyLost) {
        questionAnswered.invoke();
        moneyAdjustmentData.invoke(player, -moneyLost);
        moneyAdjustmentUI.invoke(player);
        moneyChangedHands.invoke();
        Destroy(gameObject);
    }
}
