using TMPro;
using UnityEngine;

public class BuildingCounter : MonoBehaviour {
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private TextMeshProUGUI counterText;



    #region MonoBehaviour
    private void Start() {
        updateCounter();
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesVisualRefresh(updateCounterListener);
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(updateCounterListener);
    }
    private void OnDestroy() {
        ManagePropertiesEventHub.Instance.unsub_ManagePropertiesVisualRefresh(updateCounterListener);
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(updateCounterListener);
    }
    #endregion



    #region private
    private void updateCounterListener() {
        updateCounter();
    }
    private void updateCounterListener(PlayerInfo playerInfo, bool regularRefresh) {
        updateCounter();
        if (
            !regularRefresh
            && buildingType == ManagePropertiesPanel.Instance.BuildingTypeAuctioned
            && GameState.game.BankInfo.buildingsRemaining(buildingType) == 0
        ) {
            ManagePropertiesEventHub.Instance.call_RemainingBuildingsPlaced();
        }
    }
    private void updateCounter() {
        int remaining = GameState.game.BankInfo.buildingsRemaining(buildingType);
        counterText.text = remaining.ToString();
    }
    #endregion
}
