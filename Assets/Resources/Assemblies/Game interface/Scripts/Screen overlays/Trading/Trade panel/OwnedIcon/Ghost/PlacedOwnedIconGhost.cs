
public class PlacedOwnedIconGhost : OwnedIconGhost {
    private UnplacedOwnedIcon sourceOwnedIcon;
    private ToBeTradedColumn toBeTradedColumn;



    public override UnplacedOwnedIcon OwnedIconSource => sourceOwnedIcon;
    public override ToBeTradedColumn ToBeTradedColumn => toBeTradedColumn;
    public override void ghostSetup() {
        PlacedOwnedIcon placedOwnedIcon = GetComponentInParent<PlacedOwnedIcon>();
        sourceOwnedIcon = placedOwnedIcon.OwnedIconSource;
        toBeTradedColumn = getToBeTradedColumn();
        TradableInfo tradableInfo = sourceOwnedIcon.TradableInfo;
        OwnedIconSetup ownedIconSetup = new(
            TextMesh,
            BackgroundColourImage,
            IconGetter,
            SquarePanelTransform
        );
        ownedIconSetup.setup(tradableInfo);
    }
}
