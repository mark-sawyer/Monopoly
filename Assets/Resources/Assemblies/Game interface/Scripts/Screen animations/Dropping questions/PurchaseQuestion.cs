using System.Collections;
using TMPro;
using UnityEngine;

public class PurchaseQuestion : ScreenAnimation<PlayerInfo, PropertyInfo> {
    #region GameEvents
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private GameEvent moneyChangedHands;
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
        playerObtainedProperty.invoke(playerInfo, propertyInfo);
        moneyAdjustment.invoke(playerInfo, -propertyInfo.Cost);
        moneyChangedHands.invoke();
        removeScreenAnimation.invoke();
    }
    public void noClicked() {
        removeScreenAnimation.invoke();
    }
    #endregion
}
