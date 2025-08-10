using System.Linq;
using UnityEngine;

public class RDEstateGroupSection : MonoBehaviour {
    [SerializeField] private RDEstateSection[] estateSections;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private EstateColour estateColour;
    [SerializeField] private GameObject sellAllBuildingsButtonGameObject;
    [SerializeField] private SellAllBuildingsButton sellAllBuildingsButton;
    [SerializeField] private Transform sellAllBuildingsButtonColourTransform;
    private const float NO_PROPERTIES_ALPHA = 0.25f;



    #region MonoBehaviour
    private void Start() {
        void turnOnOwnedSections(EstateGroupColours estateGroupColours) {
            PlayerInfo playerInfo = GameState.game.TurnPlayer;
            foreach (RDEstateSection estateSection in estateSections) {
                EstateInfo estateInfo = estateSection.EstateInfo;
                if (playerInfo.ownsProperty(estateInfo)) {
                    estateSection.gameObject.SetActive(true);
                    estateSection.setup(estateGroupColours);
                }
                else {
                    estateSection.gameObject.SetActive(false);
                }
            }
        }
        void setPanelColour(EstateGroupColours estateGroupColours) {
            Color panelColour = estateGroupColours.PanelColour.Colour;
            PanelRecolourer panelRecolourer = new PanelRecolourer(panelTransform);

            int owned = estateSections.Count(x => x.gameObject.activeSelf);
            if (owned == 0) {
                panelColour.a = NO_PROPERTIES_ALPHA;
                sellAllBuildingsButtonGameObject.SetActive(false);
            }
            else {
                PanelRecolourer buttonRecolourer = new PanelRecolourer(sellAllBuildingsButtonColourTransform);
                buttonRecolourer.recolour(estateGroupColours.HighlightColour.Colour);
            }
            panelRecolourer.recolour(panelColour);
        }


        EstateGroupColours estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateColour);
        EstateGroupInfo estateGroupInfo = EstateGroupDictionary.Instance.lookupInfo(estateColour);
        turnOnOwnedSections(estateGroupColours);
        setPanelColour(estateGroupColours);
        sellAllBuildingsButton.setup(estateGroupInfo);
        sellAllBuildingsButton.adjustCorrectInteractability();
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(refreshVisuals);
    }
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(refreshVisuals);
    }
    #endregion



    #region private
    private void refreshVisuals() {
        foreach (RDEstateSection rdEstateSection in estateSections) {
            if (!rdEstateSection.gameObject.activeSelf) continue;

            rdEstateSection.refreshVisual();
        }
        sellAllBuildingsButton.adjustCorrectInteractability();
    }
    #endregion
}
