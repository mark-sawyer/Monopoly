using UnityEngine;

public class ToBeTradedSpace : MonoBehaviour {
    [SerializeField] private GameObject emptySpaceGameObject;
    [SerializeField] private GameObject placedOwnedIconGameObject;
    [SerializeField] private OwnedIconReceiver ownedIconReceiver;
    [SerializeField] private PlacedOwnedIcon placedOwnedIcon;



    public bool ReceiverOn => emptySpaceGameObject.activeSelf;
    public bool FilledOn => placedOwnedIconGameObject.activeSelf;
    public PlacedOwnedIcon PlacedOwnedIcon => placedOwnedIcon;
    public void setup(PlayerInfo playerInfo) {
        ownedIconReceiver.setup(playerInfo);
    }
    public void turnOnEmpty() {
        emptySpaceGameObject.SetActive(true);
        placedOwnedIconGameObject.SetActive(false);
    }
    public void turnOnFill() {
        emptySpaceGameObject.SetActive(false);
        placedOwnedIconGameObject.SetActive(true);
    }
    public void turnOff() {
        emptySpaceGameObject.SetActive(false);
        placedOwnedIconGameObject.SetActive(false);
    }
    public void changeIcon(UnplacedOwnedIcon ownedIconSource) {
        ownedIconReceiver.changeIcon(ownedIconSource);
    }
    public TradableInfo getTradableInfo() {
        UnplacedOwnedIcon unplacedOwnedIcon = placedOwnedIcon.OwnedIconSource;
        return unplacedOwnedIcon.TradableInfo;
    }
}
