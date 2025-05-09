using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Panel : MonoBehaviour {
    private enum PartType {
        EDGE,
        CORNER,
        CENTRE
    }
    #region properties
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
    [SerializeField] private PanelSprites panelSprites;
    private float panelCornerSize;



    #region MonoBehaviour
    private void Start() {
        adjust();
    }
    #endregion



    #region public
    public void adjust() {
        setSprites();
        panelCornerSize = getPanelCornerSize();
        initialiseDimensions();
        updatePositions();
        updateCentreAndEdgeScales();
    }
    #endregion



    #region private
    private float getPanelCornerSize() {
        Image image = TopLeft.GetComponent<Image>();
        Sprite sprite = image.sprite;
        float size = sprite.rect.height;
        return size / 10f;
    }
    private void setSprites() {
        TopLeft.GetComponent<Image>().sprite = panelSprites.CornerSprite;
        Top.GetComponent<Image>().sprite = panelSprites.EdgeSprite;
        TopRight.GetComponent<Image>().sprite = panelSprites.CornerSprite;
        Left.GetComponent<Image>().sprite = panelSprites.EdgeSprite;
        Centre.GetComponent<Image>().sprite = panelSprites.CentreSprite;
        Right.GetComponent<Image>().sprite = panelSprites.EdgeSprite;
        BottomLeft.GetComponent<Image>().sprite = panelSprites.CornerSprite;
        Bottom.GetComponent<Image>().sprite = panelSprites.EdgeSprite;
        BottomRight.GetComponent<Image>().sprite = panelSprites.CornerSprite;
    }
    private void initialiseDimensions() {
        void setDimensions(RectTransform rt, PartType pt) {
            rt.sizeDelta = new Vector2(
                pt == PartType.CORNER ? panelCornerSize : 1,
                pt == PartType.CORNER || pt == PartType.EDGE ? panelCornerSize : 1
            );
        }
        setDimensions(TopLeft, PartType.CORNER);
        setDimensions(Top, PartType.EDGE);
        setDimensions(TopRight, PartType.CORNER);
        setDimensions(Left, PartType.EDGE);
        setDimensions(Centre, PartType.CENTRE);
        setDimensions(Right, PartType.EDGE);
        setDimensions(BottomLeft, PartType.CORNER);
        setDimensions(Bottom, PartType.EDGE);
        setDimensions(BottomRight, PartType.CORNER);
    }
    private void updatePositions() {
        void updatePosition(RectTransform rt, float xScale, float yScale) {
            rt.anchoredPosition = new Vector3(
                xScale * panelCornerSize / 2f,
                yScale * panelCornerSize / 2f,
                0
            );
        }
        updatePosition(TopLeft, 1, -1);
        updatePosition(Top, 0, -1);
        updatePosition(TopRight, -1, -1);
        updatePosition(Left, 1, 0);
        updatePosition(Right, -1, 0);
        updatePosition(BottomLeft, 1, 1);
        updatePosition(Bottom, 0, 1);
        updatePosition(BottomRight, -1, 1);
    }
    private void updateCentreAndEdgeScales() {
        void updateEdgeScale(RectTransform rt, float size) {
            rt.localScale = new Vector3(
                size - (2f * panelCornerSize),
                1f,
                1f
            );
        }

        float width = ((RectTransform)transform).rect.width;
        float height = ((RectTransform)transform).rect.height;
        updateEdgeScale(Top, width);
        updateEdgeScale(Left, height);
        updateEdgeScale(Right, height);
        updateEdgeScale(Bottom, width);
        Centre.localScale = new Vector3(
            width - (2f * panelCornerSize),
            height - (2f * panelCornerSize),
            1f
        );
    }
    #endregion
}
