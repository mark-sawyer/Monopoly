using UnityEngine;

public class WinnerAnnouncement : ScreenOverlay<PlayerInfo> {
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private RectTransform fallingSectionRT;
    private PlayerInfo winner;



    #region ScreenOverlay
    public override void setup(PlayerInfo winner) {
        this.winner = winner;
        tokenIcon.setup(winner.Token, winner.Colour);
    }
    public override void appear() {
        SoundPlayer.Instance.play_Congratulations(winner);
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(fallingSectionRT);
        StartCoroutine(screenOverlayDropper.drop());
    }
    #endregion
}
