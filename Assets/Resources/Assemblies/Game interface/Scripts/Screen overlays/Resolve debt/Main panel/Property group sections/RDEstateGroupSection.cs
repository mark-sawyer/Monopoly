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
    private PlayerInfo debtor;
    private EstateGroupColours estateGroupColours;
    private EstateGroupInfo estateGroupInfo;



    #region MonoBehaviour
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(refreshVisuals);
    }
    #endregion



    #region public
    public void setup(PlayerInfo debtor) {
        this.debtor = debtor;
        estateGroupColours = EstateGroupDictionary.Instance.lookupColour(estateColour);
        estateGroupInfo = EstateGroupDictionary.Instance.lookupInfo(estateColour);
        sellAllBuildingsButton.setup(estateGroupInfo, debtor);
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(refreshVisuals);

        refreshVisuals();
    }
    #endregion



    #region private
    private void refreshVisuals() {
        turnOnOwnedSections();
        setPanelColour();
        sellAllBuildingsButton.adjustCorrectInteractability();
    }
    private void turnOnOwnedSections() {
        foreach (RDEstateSection estateSection in estateSections) {
            EstateInfo estateInfo = estateSection.EstateInfo;
            if (debtor.ownsProperty(estateInfo)) {
                estateSection.gameObject.SetActive(true);
                estateSection.setup(debtor, estateGroupColours);
            }
            else {
                estateSection.gameObject.SetActive(false);
            }
        }
    }
    private void setPanelColour() {
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
    #endregion
}
