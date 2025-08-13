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
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        screenOverlayDropper.adjustSize();
        StartCoroutine(screenOverlayDropper.drop());
    }
    #endregion



    #region public
    public void backClicked() {
        ScreenOverlayEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        ScreenOverlayEventHub.Instance.call_PlayerNumberSelection();
    }
    #endregion
}
