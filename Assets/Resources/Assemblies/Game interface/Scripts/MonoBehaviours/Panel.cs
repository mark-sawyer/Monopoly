using UnityEngine;

[ExecuteAlways]
public class Panel : MonoBehaviour {
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform centre;
    [SerializeField] private RectTransform right;
    [SerializeField] private RectTransform bottom;



    private void OnEnable() {
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
            size - (2f * InterfaceConstants.PANEL_SQUARE_SIZE),
            1f,
            1f
        );
    }
    private void updateCentreScale(RectTransform rectTransform, float width, float height) {
        rectTransform.localScale = new Vector3(
            width - (2f * InterfaceConstants.PANEL_SQUARE_SIZE),
            height - (2f * InterfaceConstants.PANEL_SQUARE_SIZE),
            1f
        );
    }
}
