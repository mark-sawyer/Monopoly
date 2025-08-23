using UnityEngine;
using UnityEngine.UI;

public class TokenSelection : ScreenOverlay<int> {
    [SerializeField] private TokenReceiverSpawner tokenReceiverSpawner;
    [SerializeField] private Button backButton;



    #region ScreenOverlay
    public override void setup(int numberOfPlayers) {
        tokenReceiverSpawner.setup(numberOfPlayers);
    }
    public override void appear() {
        SoundPlayer.Instance.play_QuestionChime();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        StartCoroutine(screenOverlayDropper.drop());
    }
    #endregion



    #region public
    public void backClicked() {
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        ScreenOverlayStarterEventHub.Instance.call_PlayerNumberSelection();
    }
    #endregion
}
