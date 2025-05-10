using TMPro;
using UnityEngine;

public class PurchaseQuestion : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private TokenIcon tokenIcon;

    public void setup(PlayerInfo player, PropertyInfo property, TokenVisualManager tokenVisualManager) {
        purchaseText.text = "PURCHASE FOR $" + property.Cost.ToString();
        tokenIcon.setup(player, tokenVisualManager);
    }
}
