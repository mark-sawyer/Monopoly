using TMPro;
using UnityEngine;

public class BuildingCounter : MonoBehaviour {
    private enum BuildingType {
        HOUSE,
        HOTEL
    }
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
    private void updateCounterListener(PlayerInfo playerInfo) {
        updateCounter();
    }
    private void updateCounter() {
        int remaining = buildingType == BuildingType.HOUSE
            ? GameState.game.BankInfo.HousesRemaining
            : GameState.game.BankInfo.HotelsRemaining;

        counterText.text = remaining.ToString();
    }
    #endregion
}
