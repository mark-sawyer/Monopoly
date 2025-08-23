using UnityEngine;
using UnityEngine.UI;

public class ResolveDebtTradeButton : MonoBehaviour {
    [SerializeField] private Button button;
    private PlayerInfo debtor;



    #region MonoBehaviour
    private void Start() {
        ResolveDebtEventHub.Instance.sub_ResolveDebtPanelLowered(turnOnButton);
    }
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtPanelLowered(turnOnButton);
    }
    #endregion



    #region public
    public void setup(PlayerInfo debtor) {
        this.debtor = debtor;
    }
    #endregion



    #region private
    private void turnOnButton() {
        button.interactable = debtor.DebtInfo != null;
    }
    #endregion
}
