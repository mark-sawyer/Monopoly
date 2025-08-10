using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class OwnedIconGhost : DraggableGhost {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image backgroundColourImage;
    [SerializeField] private Transform squarePanelTransform;
    [SerializeField] private OwnedIconIconManager iconGetter;



    public abstract UnplacedOwnedIcon OwnedIconSource { get; }
    protected override void released(bool received) {
        if (!received) {
            OwnedIconSource.becomeActive();
        }
    }
    protected TextMeshProUGUI TextMesh => textMesh;
    protected Image BackgroundColourImage => backgroundColourImage;
    protected Transform SquarePanelTransform => squarePanelTransform;
    protected OwnedIconIconManager IconGetter => iconGetter;
}
