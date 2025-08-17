using UnityEngine;

public class EscapeMenu : ScreenOverlay {
    public override void appear() {
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        screenOverlayDropper.adjustSize();
        StartCoroutine(screenOverlayDropper.drop());
    }
    public void continueClicked() {
        ScreenOverlayEventHub.Instance.call_ContinueClicked();
        ScreenOverlayEventHub.Instance.call_RemoveScreenOverlay();
    }
    public void quitClicked() {
        Application.Quit();
    }
}
