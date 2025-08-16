using UnityEngine;
using UnityEngine.UI;

public class ButtonInManageProperties : MonoBehaviour {
    [SerializeField] private Button button;

    private void Start() {
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(turnOffButton);
        AuctionEventHub.Instance.sub_AuctionRemainingBuildingsButtonClicked(turnOffButton);
    }
    private void turnOffButton() {
        button.interactable = false;
    }
}
