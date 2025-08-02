
public class UnplacedOwnedIconGhost : OwnedIconGhost {
    private UnplacedOwnedIcon sourceOwnedIcon;



    public override UnplacedOwnedIcon OwnedIconSource => sourceOwnedIcon;
    public override void ghostSetup() {
        sourceOwnedIcon = GetComponentInParent<UnplacedOwnedIcon>();
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
