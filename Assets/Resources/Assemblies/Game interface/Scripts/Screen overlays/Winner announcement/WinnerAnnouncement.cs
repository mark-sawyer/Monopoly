using UnityEngine;

public class WinnerAnnouncement : ScreenOverlay<PlayerInfo> {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private RectTransform fallingSectionRT;



    #region ScreenOverlay
    public override void appear() {
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(fallingSectionRT);
        screenOverlayDropper.adjustSize();
        StartCoroutine(screenOverlayDropper.drop());
    }
    public override void setup(PlayerInfo winner) {
        tokenIcon.setup(winner.Token, winner.Colour);
    }
    #endregion
}
