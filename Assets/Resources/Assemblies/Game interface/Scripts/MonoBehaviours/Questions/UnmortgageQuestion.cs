using TMPro;
using UnityEngine;

public class UnmortgageQuestion : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI unmortgageCostText;
    [SerializeField] private TextMeshProUGUI keepMortgagedCostText;
    [SerializeField] private TokenIcon tokenIcon;

    public void setup(PlayerInfo player, PropertyInfo property, TokenVisualManager tokenVisualManager) {
        unmortgageCostText.text = "$" + getCostString(1.1f, property.MortgageValue);
        keepMortgagedCostText.text = "$" + getCostString(0.1f, property.MortgageValue);
        tokenIcon.setup(player, tokenVisualManager);
    }

    private string getCostString(float scalar, int mv) {
        float value = scalar * mv + 1e-3f;
        float rounded = Mathf.Round(value);
        int asInt = (int)rounded;
        return asInt.ToString();
    }
}
