using UnityEngine;
using UnityEngine.UI;

public class ButtonInResolveDebt : MonoBehaviour {
    [SerializeField] private Button button;
    private bool interactabilityStore;



    #region MonoBehaviour
    private void Awake() {
        ResolveDebtEventHub.Instance.sub_PanelInTransit(storeInteractabilityAndTurnOff);
        ResolveDebtEventHub.Instance.sub_ResolveDebtPanelLowered(renewInteractabilityStatus);
        ResolveDebtEventHub.Instance.sub_DeclareBankruptcyButtonClicked(turnOffButton);
    }
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_PanelInTransit(storeInteractabilityAndTurnOff);
        ResolveDebtEventHub.Instance.unsub_ResolveDebtPanelLowered(renewInteractabilityStatus);
        ResolveDebtEventHub.Instance.unsub_DeclareBankruptcyButtonClicked(turnOffButton);
    }
    #endregion



    #region private
    private void storeInteractabilityAndTurnOff() {
        interactabilityStore = button.interactable;
        button.interactable = false;
    }
    private void renewInteractabilityStatus() {
        button.interactable = interactabilityStore;
    }
    private void turnOffButton() {
        button.interactable = false;
    }
    #endregion
}
