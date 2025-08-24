using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionRemainingBuildingsButton : MonoBehaviour {
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private Button button;



    #region MonoBehaviour
    private void Start() {
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesVisualRefresh(refreshInteractability);
    }
    #endregion



    #region public
    public void buttonClicked() {
        ManagePropertiesPanel.Instance.BuildingTypeAuctioned = buildingType;
        AuctionEventHub.Instance.call_AuctionRemainingBuildingsButtonClicked();
    }
    #endregion



    #region private
    private void refreshInteractability(PlayerInfo pi, bool regularRefresh) {
        int getRemainingBuildings() {
            return buildingType == BuildingType.HOUSE
                ? GameState.game.BankInfo.HousesRemaining
                : GameState.game.BankInfo.HotelsRemaining;
        }


        if (!regularRefresh) {
            button.interactable = false;
            return;
        }

        int remaining = getRemainingBuildings();
        if (remaining == 0) {
            button.interactable = false;
            return;
        }

        bool canPressButton = false;
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        int playersCapableOfAddingRemaining = 0;
        foreach (PlayerInfo playerInfo in activePlayers) {
            int capableOfAdding = playerInfo.buildingsCanAdd(buildingType);
            if (capableOfAdding >= remaining) playersCapableOfAddingRemaining++;
            if (playersCapableOfAddingRemaining == 2) {
                canPressButton = true;
                break;
            }
        }
        button.interactable = canPressButton;
    }
    #endregion
}
