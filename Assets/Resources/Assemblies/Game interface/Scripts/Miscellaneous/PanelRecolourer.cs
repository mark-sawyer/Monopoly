using System;
using System.Collections;
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
    public IEnumerator fadeAway(int frames) {
        Color panelColour = panelTransform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        float startAlpha = panelColour.a;
        Func<float, float> getAlpha = LinearValue.getFunc(startAlpha, 0f, frames);
        for (int i = 1; i <= frames; i++) {
            float alpha = getAlpha(i);
            panelColour.a = alpha;
            recolour(panelColour);
            yield return null;
        }
        panelColour.a = 0;
        recolour(panelColour);
    }
}
