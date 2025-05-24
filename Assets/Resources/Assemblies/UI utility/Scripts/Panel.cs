using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
internal class Panel : UIAutoUpdater, TypeSettable<Color>, TypeSettable<float> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private float radius;
    [SerializeField] private Color colour;
    private float lastRadius;
    private Color lastColour;
    private Vector2 lastSize;
    #region public properties
    public Color Colour {
        get { return colour; }
        set { colour = value; }
    }
    public float Radius {
        get {
            float minSide = Mathf.Min(rt.rect.width, rt.rect.height);
            return Mathf.Clamp(radius, 0f, minSide / 2f);
        }
    }
    #endregion
    #region protected properties
    protected virtual RectTransform TopLeft { get => (RectTransform)transform.GetChild(0).GetChild(0); }
    protected virtual RectTransform Top { get => (RectTransform)transform.GetChild(0).GetChild(1); }
    protected virtual RectTransform TopRight { get => (RectTransform)transform.GetChild(0).GetChild(2); }
    protected virtual RectTransform Left { get => (RectTransform)transform.GetChild(0).GetChild(3); }
    protected virtual RectTransform Centre { get => (RectTransform)transform.GetChild(0).GetChild(4); }
    protected virtual RectTransform Right { get => (RectTransform)transform.GetChild(0).GetChild(5); }
    protected virtual RectTransform BottomLeft { get => (RectTransform)transform.GetChild(0).GetChild(6); }
    protected virtual RectTransform Bottom { get => (RectTransform)transform.GetChild(0).GetChild(7); }
    protected virtual RectTransform BottomRight { get => (RectTransform)transform.GetChild(0).GetChild(8); }
    #endregion



    #region UIAutoUpdater
    public override void updateUI() {
        setCornerSizes(new Vector2(Radius, Radius));
        setEdgeAndCentreSizes(rt.rect.width, rt.rect.height);
        setColours();
    }
    public override bool changeOccurred() {
        return (lastRadius != Radius) ||
            (lastColour != colour) ||
            (lastSize != new Vector2(rt.rect.width, rt.rect.height));
    }
    public override void updateLastState() {
        lastRadius = Radius;
        lastColour = colour;
        lastSize = new Vector2(rt.rect.width, rt.rect.height);
    }
    #endregion



    #region TypeSettable
    public void setType(Color colour) {
        this.colour = colour;
    }
    public void setType(float f) {
        radius = f;
    }
    #endregion



    #region private
    private void setCornerSizes(Vector2 cornerSize) {
        TopLeft.sizeDelta = cornerSize;
        TopRight.sizeDelta = cornerSize;
        BottomLeft.sizeDelta = cornerSize;
        BottomRight.sizeDelta = cornerSize;
    }
    #endregion



    #region protected
    protected virtual void setEdgeAndCentreSizes(float width, float height) {
        Top.sizeDelta = new Vector2(width - 2 * Radius, Radius);
        Left.sizeDelta = new Vector2(Radius, height - 2 * Radius);
        Right.sizeDelta = new Vector2(Radius, height - 2 * Radius);
        Centre.sizeDelta = new Vector2(width - 2 * Radius, height - 2 * Radius);
        Bottom.sizeDelta = new Vector2(width - 2 * Radius, Radius);
    }
    protected virtual void setColours() {
        for (int i = 0; i < 9; i++) {
            Image image = transform.GetChild(0).GetChild(i).GetComponent<Image>();
            if (image != null) image.color = colour;
        }
    }
    #endregion
}
