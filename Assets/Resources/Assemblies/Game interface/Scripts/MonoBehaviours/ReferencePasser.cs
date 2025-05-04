using UnityEngine;
using UnityEngine.UI;

public class ReferencePasser : MonoBehaviour {
    [SerializeField] private Button rollButton;
    [SerializeField] private DieVisual dieVisual;
    [SerializeField] private TokenVisualManager tokenVisualManager;
    [SerializeField] private SpaceVisualManager spaceVisualManager;



    public Button getRollButton() {
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
