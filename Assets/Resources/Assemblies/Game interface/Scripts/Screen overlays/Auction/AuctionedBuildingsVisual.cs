using TMPro;
using UnityEngine;

public class AuctionedBuildingsVisual : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private BuildingType buildingType;



    private void Start() {
        int remaining = buildingType == BuildingType.HOUSE
            ? GameState.game.BankInfo.HousesRemaining
            : GameState.game.BankInfo.HotelsRemaining;
        remainingText.text = "x" + remaining.ToString();
    }
}
