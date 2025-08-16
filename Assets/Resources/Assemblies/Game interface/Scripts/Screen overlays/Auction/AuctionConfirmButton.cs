using UnityEngine;
using UnityEngine.UI;

public class AuctionConfirmButton : MonoBehaviour {
    [SerializeField] private Button button;



    private void Start() {
        button.interactable = false;
        AuctionEventHub.Instance.sub_BidMade(setInteractable);
    }
    private void OnDestroy() {
        AuctionEventHub.Instance.unsub_BidMade(setInteractable);
    }
    private void setInteractable(PlayerInfo playerInfo, int bid) {
        button.interactable = true;
        AuctionEventHub.Instance.unsub_BidMade(setInteractable);
    }
}
