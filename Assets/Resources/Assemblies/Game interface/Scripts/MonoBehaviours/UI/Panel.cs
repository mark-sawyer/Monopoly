using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Panel : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    [SerializeField] private int radius;
    [SerializeField] private Color colour;
    #region public properties
    public Color Colour => colour;
    public int Radius => radius;
    #endregion
    #region private properties
    private RectTransform TopLeft { get => (RectTransform)transform.GetChild(0); }
    private RectTransform Top { get => (RectTransform)transform.GetChild(1); }
    private RectTransform TopRight { get => (RectTransform)transform.GetChild(2); }
    private RectTransform Left { get => (RectTransform)transform.GetChild(3); }
    private RectTransform Centre { get => (RectTransform)transform.GetChild(4); }
    private RectTransform Right { get => (RectTransform)transform.GetChild(5); }
    private RectTransform BottomLeft { get => (RectTransform)transform.GetChild(6); }
    private RectTransform Bottom { get => (RectTransform)transform.GetChild(7); }
    private RectTransform BottomRight { get => (RectTransform)transform.GetChild(8); }
    #endregion



    #region MonoBehaviour
    private void OnEnable() {
        adjust();
    }
    #endregion



    #region public
    public void adjust() {
        setCornerSizes(new Vector2(radius, radius));
        setEdgeAndCentreSizes(rt.rect.width, rt.rect.height);
        setColours();
    }
    #endregion



    #region private
    private void setCornerSizes(Vector2 cornerSize) {
        TopLeft.sizeDelta = cornerSize;
        TopRight.sizeDelta = cornerSize;
        BottomLeft.sizeDelta = cornerSize;
        BottomRight.sizeDelta = cornerSize;
    }
    private void setEdgeAndCentreSizes(float width, float height) {
        Top.sizeDelta = new Vector2(width - 2 * radius, radius);
        Left.sizeDelta = new Vector2(radius, height - 2 * radius);
        Right.sizeDelta = new Vector2(radius, height - 2 * radius);
        Centre.sizeDelta = new Vector2(width - 2 * radius, height - 2 * radius);
        Bottom.sizeDelta = new Vector2(width - 2 * radius, radius);
    }
    private void setColours() {
        for (int i = 0; i < 9; i++) {
            transform.GetChild(i).GetComponent<Image>().color = colour;
        }
    }
    #endregion
}
