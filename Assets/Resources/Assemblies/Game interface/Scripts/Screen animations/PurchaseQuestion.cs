using System.Collections;
using TMPro;
using UnityEngine;

public class PurchaseQuestion : ScreenAnimation<PlayerInfo, PropertyInfo> {
    #region GameEvents
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private SoundEvent moneyChangedHands;
    [SerializeField] private SoundEvent propertyAdjustmentBloop;
    #endregion
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
        moneyAdjustment.invoke(playerInfo, -propertyInfo.Cost);
        moneyChangedHands.play();
        removeScreenAnimation.invoke();
        WaitFrames.Instance.exe(80, () => propertyAdjustmentBloop.play());
        WaitFrames.Instance.exe(100, () => playerObtainedProperty.invoke(playerInfo, propertyInfo));
    }
    public void noClicked() {
        removeScreenAnimation.invoke();
    }
    #endregion
}
