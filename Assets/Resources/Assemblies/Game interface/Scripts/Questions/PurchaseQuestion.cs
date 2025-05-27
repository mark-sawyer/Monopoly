using TMPro;
using UnityEngine;

public class PurchaseQuestion : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;
    #region GameEvents
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerObtainedPropertyData;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerObtainedPropertyUI;
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustmentData;
    [SerializeField] private GameEvent<PlayerInfo> moneyAdjustmentUI;
    [SerializeField] private GameEvent questionAnswered;
    [SerializeField] private GameEvent moneyChangedHands;
    #endregion
    private PlayerInfo player;
    private PropertyInfo property;



    #region public
    public void setup(PlayerInfo player, PropertyInfo property) {
        this.player = player;
        this.property = property;
        purchaseText.text = "PURCHASE FOR $" + property.Cost.ToString();
        tokenIcon.setup(player.Token, player.Colour);
    }
    public void yesClicked() {
        questionAnswered.invoke();
        playerObtainedPropertyData.invoke(player, property);
        playerObtainedPropertyUI.invoke(player, property);
        moneyAdjustmentData.invoke(player, -property.Cost);
        moneyAdjustmentUI.invoke(player);
        moneyChangedHands.invoke();
        Destroy(gameObject);
    }
    public void noClicked() {
        questionAnswered.invoke();
        Destroy(gameObject);
    }
    #endregion
}
