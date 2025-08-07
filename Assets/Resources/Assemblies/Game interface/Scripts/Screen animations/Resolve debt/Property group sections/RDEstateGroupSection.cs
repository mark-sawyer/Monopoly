using UnityEngine;

public class RDEstateGroupSection : MonoBehaviour {
    [SerializeField] private RDEstateSection[] estateSections;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private EstateColour estateColour;
    [SerializeField] private GameObject sellAllBuildingsButton;
    [SerializeField] private Transform sellAllBuildingsButtonColourTransform;
    private const float NO_PROPERTIES_ALPHA = 0.25f;

    private void Start() {
        EstateGroupColours estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateColour);
        Color panelColour = estateGroupColours.PanelColour.Colour;
        PanelRecolourer panelRecolourer = new PanelRecolourer(panelTransform);

        int owned = 0;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        foreach (RDEstateSection estateSection in estateSections) {
            EstateInfo estateInfo = estateSection.EstateInfo;
            if (playerInfo.ownsProperty(estateInfo)) {
                estateSection.gameObject.SetActive(true);
                estateSection.setup(estateGroupColours);
                owned++;
            }
            else {
                estateSection.gameObject.SetActive(false);
            }
        }

        if (owned == 0) {
            panelColour.a = NO_PROPERTIES_ALPHA;
            sellAllBuildingsButton.SetActive(false);
        }
        else {
            PanelRecolourer buttonRecolourer = new PanelRecolourer(sellAllBuildingsButtonColourTransform);
            buttonRecolourer.recolour(estateGroupColours.HighlightColour.Colour);
        }
        panelRecolourer.recolour(panelColour);
    }
}
