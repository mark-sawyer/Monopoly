using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RDOtherPropertyGroupSection : MonoBehaviour {
    [SerializeField] private ScriptableObject propertyGroupSO;
    [SerializeField] private Transform panelTransform;
    [SerializeField] private RDOtherPropertySection[] otherPropertySections;
    private const float NO_PROPERTIES_ALPHA = 0.25f;



    #region MonoBehaviour
    private void Start() {
        void turnOnOwnedSections() {
            PlayerInfo playerInfo = GameState.game.TurnPlayer;
            foreach (RDOtherPropertySection otherPropertySection in otherPropertySections) {
                PropertyInfo propertyInfo = otherPropertySection.PropertyInfo;
                if (playerInfo.ownsProperty(propertyInfo)) {
                    otherPropertySection.gameObject.SetActive(true);
                }
                else {
                    otherPropertySection.gameObject.SetActive(false);
                }
            }
        }
        void setPanelColour() {
            PropertyGroupInfo propertyGroupInfo = (PropertyGroupInfo)propertyGroupSO;            
            int owned = otherPropertySections.Count(x => x.gameObject.activeSelf);
            if (owned == 0) {
                Color colour = panelTransform.GetChild(0).GetChild(0).GetComponent<Image>().color;
                colour.a = NO_PROPERTIES_ALPHA;
                PanelRecolourer panelRecolourer = new PanelRecolourer(panelTransform);
                panelRecolourer.recolour(colour);
            }
        }


        turnOnOwnedSections();
        setPanelColour();
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(refreshVisuals);
    }
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(refreshVisuals);
    }
    #endregion



    #region private
    private void refreshVisuals() {
        foreach (RDOtherPropertySection otherPropertySection in otherPropertySections) {
            if (!otherPropertySection.gameObject.activeSelf) continue;

            otherPropertySection.refreshVisual();
        }
    }
    #endregion
}
