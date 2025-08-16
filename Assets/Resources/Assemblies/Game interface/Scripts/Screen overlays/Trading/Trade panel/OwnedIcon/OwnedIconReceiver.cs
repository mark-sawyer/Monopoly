using UnityEngine;

public class OwnedIconReceiver : GhostReceiver {
    [SerializeField] private Transform parent;
    [SerializeField] private PlacedOwnedIcon placedOwnedIcon;
    private UnplacedOwnedIcon ownedIconSource;
    private PlayerInfo playerOfSide;
    private TradeEventHub tradeEventHub;



    #region public
    public void setup(PlayerInfo playerOfSide) {
        this.playerOfSide = playerOfSide;
        tradeEventHub = TradeEventHub.Instance;
    }
    public override bool canReceiveThisGhost(DraggableGhost ghost) {
        OwnedIconGhost ownedIconGhost = (OwnedIconGhost)ghost;
        ownedIconSource = ownedIconGhost.OwnedIconSource;
        PlayerInfo owner = ownedIconSource.PlayerInfo;
        return owner == playerOfSide;
    }
    public override void receiveGhost(DraggableGhost ghost) {
        OwnedIconGhost ownedIconGhost = (OwnedIconGhost)ghost;
        ownedIconSource = ownedIconGhost.OwnedIconSource;
        placedOwnedIcon.gameObject.SetActive(true);
        placedOwnedIcon.setup(ownedIconSource);
        turnOnNextReceiver();
        tradeEventHub.call_TradeChanged();
        gameObject.SetActive(false);
        SoundOnlyEventHub.Instance.call_Put();
    }
    public void changeIcon(UnplacedOwnedIcon newOwnedIconSource) {
        ownedIconSource = newOwnedIconSource;
        placedOwnedIcon.setup(ownedIconSource);
    }
    #endregion



    #region private
    private void turnOnNextReceiver() {
        Transform grandparent = parent.parent;
        int parentSiblingIndex = parent.GetSiblingIndex();
        int children = grandparent.childCount;
        if (parentSiblingIndex == children - 1) return;
        Transform aunt = grandparent.GetChild(parentSiblingIndex + 1);
        Transform cousin = aunt.GetChild(0);
        cousin.gameObject.SetActive(true);
    }
    #endregion
}
