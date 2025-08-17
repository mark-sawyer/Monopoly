using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnplacedOwnedIcon : DraggableGhostSource {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image backgroundColourImage;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private OwnedIconIconManager iconGetter;
    private PlayerInfo playerInfo;
    private TradableInfo tradableInfo;



    #region DraggableGhostSource
    protected override void reactToCreatingGhost() {
        SoundOnlyEventHub.Instance.call_Take();
        int ghostIndex = transform.childCount - 1;
        Transform ghostTransform = transform.GetChild(ghostIndex);
        ghostTransform.SetParent(transform.parent);
        TradePanel.Instance.getToBeTradedColumn(playerInfo).toggleHighlightSquare(true);
        gameObject.SetActive(false);
    }
    #endregion



    #region public
    public TradableInfo TradableInfo => tradableInfo;
    public PlayerInfo PlayerInfo => playerInfo;
    public void setupOwnedIcon(TradableInfo tradableInfo, PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        OwnedIconSetup ownedIconSetup = new(
            textMesh,
            backgroundColourImage,
            iconGetter,
            squarePanelTransform
        );
        ownedIconSetup.setup(tradableInfo);
        this.tradableInfo = tradableInfo;
    }
    public void becomeActive() {
        gameObject.SetActive(true);
    }
    #endregion
}
