using TMPro;
using UnityEngine;

public class ResolveMortgage : ScreenOverlay<PlayerInfo, PropertyInfo> {
    [SerializeField] private RectTransform RT;
    [SerializeField] private RectTransform middleSectionRT;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private TextMeshProUGUI unmortgageCostText;
    [SerializeField] private TextMeshProUGUI keepMortgagedCostText;



    #region MonoBehaviour
    private void OnDestroy() {
        AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
    }
    #endregion



    #region ScreenOverlay
    public override void setup(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
        unmortgageCostText.text = "$" + propertyInfo.UnmortgageCost.ToString();
        keepMortgagedCostText.text = "$" + propertyInfo.RetainMortgageCost.ToString();
        AccompanyingVisualSpawner.Instance.spawnAndMove(middleSectionRT, propertyInfo);
    }
    public override void appear() {
        SoundPlayer.Instance.play_QuestionChime();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(RT);
        StartCoroutine(screenOverlayDropper.drop());
    }
    #endregion
}
