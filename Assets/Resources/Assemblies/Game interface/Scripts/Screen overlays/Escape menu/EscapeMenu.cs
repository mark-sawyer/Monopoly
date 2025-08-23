using UnityEngine;

public class EscapeMenu : ScreenOverlay {
    public override void appear() {
        SoundPlayer.Instance.play_Dub();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        StartCoroutine(screenOverlayDropper.drop());
    }
    public void continueClicked() {
        ScreenOverlayFunctionEventHub.Instance.call_ContinueClicked();
        ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlay();
    }
    public void quitClicked() {
        Application.Quit();
    }
}
