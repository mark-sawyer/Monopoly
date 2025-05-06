using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Panel : MonoBehaviour {
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform centre;
    [SerializeField] private RectTransform right;
    [SerializeField] private RectTransform bottom;
    private float panelSquareSize;



    private void OnEnable() {
        panelSquareSize = getPanelSquareSize();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        updateEdgeScale(top, width);
        updateEdgeScale(left, height);
        updateCentreScale(centre, width, height);
        updateEdgeScale(right, height);
        updateEdgeScale(bottom, width);
    }
    private void updateEdgeScale(RectTransform rectTransform, float size) {
        rectTransform.localScale = new Vector3(
            size - (2f * panelSquareSize),
            1f,
            1f
        );
    }
    private void updateCentreScale(RectTransform rectTransform, float width, float height) {
        rectTransform.localScale = new Vector3(
            width - (2f * panelSquareSize),
            height - (2f * panelSquareSize),
            1f
        );
    }
    private float getPanelSquareSize() {
        Transform child = transform.GetChild(0);
        Image image = child.GetComponent<Image>();
        Sprite sprite = image.sprite;
        float size = sprite.rect.height;
        return size / 10f;
    }
}
