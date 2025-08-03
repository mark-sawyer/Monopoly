using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlacedOwnedIcon : DraggableGhostSource {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image backgroundColourImage;
    [SerializeField] private OwnedIconIconManager iconGetter;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private ToBeTradedColumn toBeTradedColumn;
    private UnplacedOwnedIcon ownedIconSource;
    private TradeEventHub tradeEventHub;



    #region DraggableGhostSource
    protected override void reactToCreatingGhost() {
        Transform parent = transform.parent;
        int ghostIndex = transform.childCount - 1;
        Transform ghostTransform = transform.GetChild(ghostIndex);
        ghostTransform.SetParent(parent);
        GameObject emptySpace = parent.GetChild(0).gameObject;
        emptySpace.SetActive(true);
        gameObject.SetActive(false);
        toBeTradedColumn.shiftIconsUp();
        tradeEventHub.call_TradeChanged();
    }
    #endregion



    #region public
    public UnplacedOwnedIcon OwnedIconSource => ownedIconSource;
    public void setup(UnplacedOwnedIcon ownedIconSource) {
        this.ownedIconSource = ownedIconSource;
        TradableInfo tradableInfo = ownedIconSource.TradableInfo;
        OwnedIconSetup ownedIconSetup = new OwnedIconSetup(
            textMesh,
            backgroundColourImage,
            iconGetter,
            squarePanelTransform
        );
        ownedIconSetup.setup(tradableInfo);
        tradeEventHub = TradeEventHub.Instance;
    }
    #endregion
}
