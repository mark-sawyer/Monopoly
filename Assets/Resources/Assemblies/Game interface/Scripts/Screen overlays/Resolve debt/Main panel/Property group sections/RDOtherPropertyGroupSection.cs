using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RDOtherPropertyGroupSection : MonoBehaviour {
    [SerializeField] private ScriptableObject propertyGroupSO;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private RDOtherPropertySection[] otherPropertySections;
    private const float NO_PROPERTIES_ALPHA = 0.25f;
    private PlayerInfo debtor;



    #region MonoBehaviour
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(refreshVisuals);
    }
    #endregion



    #region public
    public void setup(PlayerInfo debtor) {
        this.debtor = debtor;
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(refreshVisuals);

        refreshVisuals();
    }
    public void refreshVisuals() {
        turnOnOwnedSections();
        setPanelColour();
    }
    #endregion



    #region private
    private void turnOnOwnedSections() {
        foreach (RDOtherPropertySection otherPropertySection in otherPropertySections) {
            PropertyInfo propertyInfo = otherPropertySection.PropertyInfo;
            if (debtor.ownsProperty(propertyInfo)) {
                otherPropertySection.gameObject.SetActive(true);
                otherPropertySection.refreshVisual();
            }
            else {
                otherPropertySection.gameObject.SetActive(false);
            }
        }
    }
    private void setPanelColour() {
        PropertyGroupInfo propertyGroupInfo = (PropertyGroupInfo)propertyGroupSO;
        int owned = otherPropertySections.Count(x => x.gameObject.activeSelf);
        if (owned == 0) {
            Color colour = panelTransform.GetChild(0).GetChild(0).GetComponent<Image>().color;
            colour.a = NO_PROPERTIES_ALPHA;
            PanelRecolourer panelRecolourer = new PanelRecolourer(panelTransform);
            panelRecolourer.recolour(colour);
        }
    }
    #endregion
}
