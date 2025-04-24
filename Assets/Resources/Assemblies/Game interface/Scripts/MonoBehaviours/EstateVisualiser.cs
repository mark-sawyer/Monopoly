using UnityEngine;

public class EstateVisualiser : MonoBehaviour {
    [SerializeField] SpriteRenderer colourBand;
    [SerializeField] Estate estate;

    private void Start() {
        EstateColour estateColour = estate.getEstateColour();
        colourBand.color = UIUtilities.estateColourToColour(estateColour);
    }
}
