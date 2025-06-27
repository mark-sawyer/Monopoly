using UnityEngine;
using UnityEngine.UI;

public class PanelRecolourer {
    private Transform panelTransform;



    public PanelRecolourer(Transform panelTransform) {
        this.panelTransform = panelTransform;
    }
    public void recolour(Color newColour) {
        Transform sectionsTransform = panelTransform.GetChild(0);
        for (int i = 0; i < sectionsTransform.childCount; i++) {
            Transform child = sectionsTransform.GetChild(i);
            Image image = child.GetComponent<Image>();
            image.color = newColour;
        }
    }
}
