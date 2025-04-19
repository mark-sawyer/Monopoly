using UnityEngine;

public class EstateVisualiser : MonoBehaviour {
    [SerializeField] SpriteRenderer colourBand;
    [SerializeField] ScriptableObject estateScriptableObject;
    EstateVisualDataGetter estateVisualDataGetter;

    private void Start() {
        estateVisualDataGetter = (EstateVisualDataGetter)estateScriptableObject;
        EstateColour estateColour = estateVisualDataGetter.getEstateColour();
        colourBand.color = EstateColourToColour.exe(estateColour);        
    }
}
