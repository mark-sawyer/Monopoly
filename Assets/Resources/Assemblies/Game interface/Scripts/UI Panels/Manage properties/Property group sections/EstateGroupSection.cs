using UnityEngine;

public class EstateGroupSection : PropertyGroupSection {
    [SerializeField] private Transform panelSectionsTransform;
    [SerializeField] private EstateColour estateColour;

    public override Color TransitionPanelColour => EstateGroupDictionary.Instance.lookupColour(estateColour).TransitionPanelColour.Colour;
    public override void setup() {
        EstateGroupColours estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateColour);
        Color colour = estateGroupColours.PanelColour.Colour;
        PanelRecolourer panelRecolourer = new PanelRecolourer(panelSectionsTransform);
        panelRecolourer.recolour(colour);
    }
}
