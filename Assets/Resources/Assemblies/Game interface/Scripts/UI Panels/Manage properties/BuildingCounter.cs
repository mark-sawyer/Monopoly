using TMPro;
using UnityEngine;

public class BuildingCounter : MonoBehaviour {
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private TextMeshProUGUI counterText;



    private void Start() {
        updateCounter(null);
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesVisualRefresh(updateCounter);
    }



    private void updateCounter(PlayerInfo playerInfo) {
        int remaining
            = buildingType == BuildingType.HOUSE
            ? GameState.game.BankInfo.HousesRemaining
            : GameState.game.BankInfo.HotelsRemaining;

        counterText.text = remaining.ToString();
    }
}
