using UnityEngine;
using UnityEngine.UI;

public class ReferencePasser : MonoBehaviour {
    [SerializeField] private MultiGraphicButton rollButton;
    [SerializeField] private DieVisual dieVisual;
    [SerializeField] private TokenVisualManager tokenVisualManager;
    [SerializeField] private SpaceVisualManager spaceVisualManager;



    public MultiGraphicButton getRollButton() {
        return rollButton;
    }
    public DieVisual getDieVisual() {
        return dieVisual;
    }
    public TokenVisualManager getTokenVisualManager() {
        return tokenVisualManager;
    }
    public SpaceVisualManager getSpaceVisualManager() {
        return spaceVisualManager;
    }
}
