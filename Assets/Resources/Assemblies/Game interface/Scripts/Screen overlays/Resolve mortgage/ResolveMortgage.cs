using TMPro;
using UnityEngine;

public class ResolveMortgage : ScreenOverlay<PlayerInfo, PropertyInfo> {
    [SerializeField] private RectTransform RT;
    [SerializeField] private RectTransform middleSectionRT;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private TextMeshProUGUI unmortgageCostText;
    [SerializeField] private TextMeshProUGUI keepMortgagedCostText;
    private PlayerInfo playerInfo;
    private PropertyInfo propertyInfo;



    #region MonoBehaviour
    private void OnDestroy() {
        AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
    }
    #endregion



    #region ScreenOverlay
    public override void appear() {
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
        AccompanyingVisualSpawner.Instance.spawnAndMove(middleSectionRT, propertyInfo);
        unmortgageCostText.text = "$" + propertyInfo.UnmortgageCost.ToString();
        keepMortgagedCostText.text = "$" + propertyInfo.RetainMortgageCost.ToString();
        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(RT);
        screenOverlayDropper.adjustSize();
        StartCoroutine(screenOverlayDropper.drop());
    }
    public override void setup(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        this.playerInfo = playerInfo;
        this.propertyInfo = propertyInfo;
    }
    #endregion
}
