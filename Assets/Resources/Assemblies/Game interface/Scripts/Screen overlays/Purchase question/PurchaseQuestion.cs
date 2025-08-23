using TMPro;
using UnityEngine;

public class PurchaseQuestion : ScreenOverlay<PlayerInfo, PropertyInfo> {
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private RectTransform middleRT;
    private PlayerInfo playerInfo;
    private PropertyInfo propertyInfo;



    #region ScreenOverlay
    public override void setup(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        this.playerInfo = playerInfo;
        this.propertyInfo = propertyInfo;
        purchaseText.text = "PURCHASE FOR $" + this.propertyInfo.Cost.ToString();
        tokenIcon.setup(this.playerInfo.Token, this.playerInfo.Colour);
    }
    public override void appear() {
        SoundPlayer.Instance.play_QuestionChime();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper((RectTransform)transform);
        StartCoroutine(screenOverlayDropper.drop());
        AccompanyingVisualSpawner.Instance.spawnAndMove(middleRT, propertyInfo);
    }
    #endregion
}
