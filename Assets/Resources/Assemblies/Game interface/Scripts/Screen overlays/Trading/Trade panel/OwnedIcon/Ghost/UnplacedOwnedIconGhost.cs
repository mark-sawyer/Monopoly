
public class UnplacedOwnedIconGhost : OwnedIconGhost {
    private UnplacedOwnedIcon sourceOwnedIcon;
    private ToBeTradedColumn toBeTradedColumn;



    public override UnplacedOwnedIcon OwnedIconSource => sourceOwnedIcon;
    public override ToBeTradedColumn ToBeTradedColumn => toBeTradedColumn;
    public override void ghostSetup() {
        sourceOwnedIcon = GetComponentInParent<UnplacedOwnedIcon>();
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
