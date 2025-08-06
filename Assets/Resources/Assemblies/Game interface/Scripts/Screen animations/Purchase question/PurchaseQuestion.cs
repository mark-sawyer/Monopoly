using System.Collections;
using TMPro;
using UnityEngine;

public class PurchaseQuestion : ScreenAnimation<PlayerInfo, PropertyInfo> {
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private RectTransform droppedQuestionRT;
    [SerializeField] private DeedSpawner deedSpawner;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;
    private PlayerInfo playerInfo;
    private PropertyInfo propertyInfo;



    #region MonoBehaviour
    private void OnEnable() {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality(droppedQuestionRT);
        droppingQuestionsFunctionality.adjustSize();
    }
    #endregion



    #region ScreenAnimation
    public override void appear() {
        StartCoroutine(droppingQuestionsFunctionality.drop());
        StartCoroutine(deedSpawner.moveDeed());
    }
    public override void setup(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        this.playerInfo = playerInfo;
        this.propertyInfo = propertyInfo;
        purchaseText.text = "PURCHASE FOR $" + this.propertyInfo.Cost.ToString();
        tokenIcon.setup(this.playerInfo.Token, this.playerInfo.Colour);
        deedSpawner.spawnDeed(propertyInfo);
    }
    #endregion



    #region public
    public void yesClicked() {
        DataUIPipelineEventHub.Instance.call_MoneyAdjustment(playerInfo, -propertyInfo.Cost);
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation();
        WaitFrames.Instance.beforeAction(
            90,
            () => DataUIPipelineEventHub.Instance.call_PlayerObtainedProperty(playerInfo, propertyInfo)
        );
    }
    public void noClicked() {
        Transform deedSpawnerTransform = deedSpawner.transform;
        Transform deedTransform = deedSpawnerTransform.GetChild(0);
        AuctionManager auctionManager = AuctionManager.Instance;
        auctionManager.takeInVisualChild(deedTransform);
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimationKeepCover();
    }
    #endregion
}
