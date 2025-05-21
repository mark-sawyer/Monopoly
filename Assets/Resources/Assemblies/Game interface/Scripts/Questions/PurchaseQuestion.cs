using TMPro;
using UnityEngine;

public class PurchaseQuestion : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPurchasedProperty;
    [SerializeField] private GameEvent playerDeclinedPurchase;
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustment;
    [SerializeField] private GameEvent questionAnswered;
    private PlayerInfo player;
    private PropertyInfo property;



    #region MonoBehaviour
    private void Start() {
        questionAnswered.Listeners += destroySelf;
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, PropertyInfo property) {
        this.player = player;
        this.property = property;
        purchaseText.text = "PURCHASE FOR $" + property.Cost.ToString();
        tokenIcon.setup(player);
    }
    public void yesClicked() {
        questionAnswered.invoke();
        playerPurchasedProperty.invoke(player, property);
        moneyAdjustment.invoke(player, -property.Cost);
    }
    public void noClicked() {
        questionAnswered.invoke();
        playerDeclinedPurchase.invoke();
    }
    #endregion



    #region private
    private void destroySelf() {
        questionAnswered.Listeners -= destroySelf;
        Destroy(gameObject);
    }
    #endregion
}
