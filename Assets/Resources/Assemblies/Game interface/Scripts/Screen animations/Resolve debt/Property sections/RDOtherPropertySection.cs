using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RDOtherPropertySection : MonoBehaviour {
    [SerializeField] private ScriptableObject propertySO;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI mortgageValueText;
    [SerializeField] private GameObject mortgagedLabel;



    #region MonoBehaviour
    private void Start() {
        mortgageValueText.text = "$" + PropertyInfo.MortgageValue.ToString();
    }
    #endregion



    #region public
    public PropertyInfo PropertyInfo => (PropertyInfo)propertySO;
    public void buttonClicked() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;

        DataEventHub.Instance.call_PropertyMortgaged(PropertyInfo);
        DataUIPipelineEventHub.Instance.call_MoneyRaisedForDebt(GameState.game.TurnPlayer, PropertyInfo.MortgageValue);
        ResolveDebtEventHub.Instance.call_ResolveDebtVisualRefresh();
    }
    public void refreshVisual() {
        bool propertyMortgaged = PropertyInfo.IsMortgaged;
        bool debtPaid = GameState.game.TurnPlayer.DebtInfo == null;

        mortgagedLabel.SetActive(propertyMortgaged);
        button.interactable = !propertyMortgaged && !debtPaid;
    }
    #endregion
}
