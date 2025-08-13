using UnityEngine;

public class GameUIPanelScaler : MonoBehaviour {
    [SerializeField] private RectTransform dicePanelRT;
    [SerializeField] private RectTransform otherOptionsPanelRT;
    private const float GAP = 11;



    #region MonoBehaviour
    private void Start() {
        Rect panelRect = ((RectTransform)transform).rect;
        Rect canvasRect = ((RectTransform)transform.parent).rect;

        float maxWidthForPanel = (canvasRect.width - canvasRect.height) / 2f;
        float minHeightForPanel = dicePanelRT.rect.height + otherOptionsPanelRT.rect.height + GAP;

        float scale;
        float horizontalScale = maxWidthForPanel / panelRect.width;
        float verticalScale = canvasRect.height / minHeightForPanel;
        scale = horizontalScale < verticalScale ? horizontalScale : verticalScale;

        dicePanelRT.localScale = new Vector3(scale, scale, scale);
        otherOptionsPanelRT.localScale = new Vector3(scale, scale, scale);
    }
    #endregion
}
