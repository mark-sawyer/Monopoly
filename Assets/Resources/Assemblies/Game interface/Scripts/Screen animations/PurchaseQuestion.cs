using System.Collections;
using TMPro;
using UnityEngine;

public class PurchaseQuestion : ScreenAnimation<PlayerInfo, PropertyInfo> {
    [SerializeField] private SoundEvent moneyChangedHands;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;
    private PlayerInfo playerInfo;
    private PropertyInfo propertyInfo;



    #region MonoBehaviour
    private void OnEnable() {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality((RectTransform)transform);
        droppingQuestionsFunctionality.adjustSize();
    }
    #endregion



    #region ScreenAnimation
    public override void appear() {
        StartCoroutine(droppingQuestionsFunctionality.drop());
    }
    public override void setup(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        this.playerInfo = playerInfo;
        this.propertyInfo = propertyInfo;
        purchaseText.text = "PURCHASE FOR $" + this.propertyInfo.Cost.ToString();
        tokenIcon.setup(this.playerInfo.Token, this.playerInfo.Colour);
    }
    #endregion



    #region public
    public void yesClicked() {
        DataEventHub.Instance.call_MoneyAdjustment(playerInfo, -propertyInfo.Cost);
        moneyChangedHands.play();
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation();
        WaitFrames.Instance.exe(
            90,
            () => DataEventHub.Instance.call_PlayerObtainedProperty(playerInfo, propertyInfo)
        );
    }
    public void noClicked() {
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation();
    }
    #endregion
}
