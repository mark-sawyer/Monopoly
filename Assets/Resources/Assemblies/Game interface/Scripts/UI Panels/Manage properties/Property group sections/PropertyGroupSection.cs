using UnityEngine;
using UnityEngine.UI;

public abstract class PropertyGroupSection : MonoBehaviour {
    [SerializeField] private PropertySection[] propertySections;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private RectTransform transitionPanel;
    [SerializeField] private Transform transitionPanelParent;
    private const float NO_PROPERTIES_ALPHA = 0.25f;



    #region MonoBehaviour
    private void Start() {
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesVisualRefresh(refreshVisuals);
        setupTransitionPanel(TransitionPanelColour);
        setup();
    }
    #endregion



    #region PropertyGroupSection
    public abstract Color TransitionPanelColour { get; }
    public virtual void setup() { }
    #endregion



    #region private
    private void setupTransitionPanel(Color colour) {
        transitionPanel.gameObject.SetActive(true);
        PanelRecolourer panelRecolourer = new PanelRecolourer(transitionPanel);
        panelRecolourer.recolour(colour);
        transitionPanel.SetParent(transitionPanelParent);
    }
    private void refreshVisuals(PlayerInfo playerInfo) {
        void adjustPanelOpacity(int owned) {
            Color panelColour = panelTransform.GetChild(0).GetChild(0).GetComponent<Image>().color;
            panelColour.a = owned == 0 ? NO_PROPERTIES_ALPHA : 1f;
            PanelRecolourer panelRecolourer = new PanelRecolourer(panelTransform);
            panelRecolourer.recolour(panelColour);
        }

        int owned = 0;
        foreach (PropertySection propertySection in propertySections) {
            PropertyInfo propertyInfo = propertySection.PropertyInfo;
            if (playerInfo.ownsProperty(propertyInfo)) {
                propertySection.gameObject.SetActive(true);
                propertySection.refreshVisual(playerInfo);
                owned++;
            }
            else {
                propertySection.gameObject.SetActive(false);
            }
        }
        adjustPanelOpacity(owned);
    }
    #endregion
}
