using UnityEngine;

public class EstateVisual : SpaceVisual {
    [SerializeField] SpriteRenderer colourBand;
    [SerializeField] Estate estate;

    private void Start() {
        EstateColour estateColour = estate.getEstateColour();
        colourBand.color = UIUtilities.estateColourToColour(estateColour);
    }
}
