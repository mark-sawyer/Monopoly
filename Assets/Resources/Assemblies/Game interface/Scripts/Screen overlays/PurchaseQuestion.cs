using TMPro;
using UnityEngine;

public class PurchaseQuestion : ScreenOverlay<PlayerInfo, PropertyInfo> {
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private RectTransform droppedQuestionRT;
    [SerializeField] private RectTransform middleRT;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;
    private PlayerInfo playerInfo;
    private PropertyInfo propertyInfo;



    #region MonoBehaviour
    private void OnEnable() {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality(droppedQuestionRT);
        droppingQuestionsFunctionality.adjustSize();
    }
    #endregion



    #region ScreenOverlay
    public override void appear() {
        StartCoroutine(droppingQuestionsFunctionality.drop());
        AccompanyingVisualSpawner.Instance.spawnAndMove(middleRT, propertyInfo);
    }
    public override void setup(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        this.playerInfo = playerInfo;
        this.propertyInfo = propertyInfo;
        purchaseText.text = "PURCHASE FOR $" + this.propertyInfo.Cost.ToString();
        tokenIcon.setup(this.playerInfo.Token, this.playerInfo.Colour);
    }
    #endregion
}
