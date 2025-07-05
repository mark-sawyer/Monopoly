using UnityEngine;
using UnityEngine.UI;

public class SellOrMortgageBuildingButton : MonoBehaviour {
    [SerializeField] private Button button;
    private EstateInfo estateInfo;



    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public void setInteractable() {
        button.interactable = estateInfo.CanRemoveBuilding;
    }
    public void buttonClicked() {
        PlayerInfo selectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        //estateAddedBuilding.invoke(estateInfo);
    }
}
