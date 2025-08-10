
public class PlacedOwnedIconGhost : OwnedIconGhost {
    private UnplacedOwnedIcon sourceOwnedIcon;



    public override UnplacedOwnedIcon OwnedIconSource => sourceOwnedIcon;
    public override void ghostSetup() {
        PlacedOwnedIcon placedOwnedIcon = GetComponentInParent<PlacedOwnedIcon>();
        sourceOwnedIcon = placedOwnedIcon.OwnedIconSource;
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
