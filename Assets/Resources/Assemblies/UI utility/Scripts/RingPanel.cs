using UnityEngine;
using UnityEngine.UI;

internal class RingPanel : Panel {
    #region protected properties
    protected override RectTransform Right { get => (RectTransform)transform.GetChild(0).GetChild(4); }
    protected override RectTransform BottomLeft { get => (RectTransform)transform.GetChild(0).GetChild(5); }
    protected override RectTransform Bottom { get => (RectTransform)transform.GetChild(0).GetChild(6); }
    protected override RectTransform BottomRight { get => (RectTransform)transform.GetChild(0).GetChild(7); }
    #endregion



    protected override void setEdgeAndCentreSizes(float width, float height) {
        Top.sizeDelta = new Vector2(width - 2 * Radius, Radius);
        Left.sizeDelta = new Vector2(Radius, height - 2 * Radius);
        Right.sizeDelta = new Vector2(Radius, height - 2 * Radius);
        Bottom.sizeDelta = new Vector2(width - 2 * Radius, Radius);
    }
    protected override void setColours() {
        for (int i = 0; i < 8; i++) {
            Image image = transform.GetChild(0).GetChild(i).GetComponent<Image>();
            if (image != null) image.color = Colour;
        }
    }
}
