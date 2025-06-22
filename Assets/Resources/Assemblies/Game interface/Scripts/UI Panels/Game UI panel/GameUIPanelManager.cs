using UnityEngine;

public class GameUIPanelManager : MonoBehaviour {
    private const float GAP = 11;


    #region MonoBehaviour
    private void Start() {
        scalePanels();
    }
    #endregion



    #region private
    private void scalePanels() {
        Rect panelRect = ((RectTransform)transform).rect;
        Rect canvasRect = ((RectTransform)transform.parent).rect;

        RectTransform dicePanelRT = (RectTransform)transform.GetChild(0);
        RectTransform otherOptionsPanelRT = (RectTransform)transform.GetChild(1);

        float maxWidthForPanel = (canvasRect.width - canvasRect.height) / 2f;
        float minHeightForPanel = dicePanelRT.rect.height + otherOptionsPanelRT.rect.height + GAP;

        float scale;
        float horizontalScale = maxWidthForPanel / panelRect.width;
        float verticalScale = canvasRect.height / minHeightForPanel;
        scale = horizontalScale < verticalScale ? horizontalScale : verticalScale;

        transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
        transform.GetChild(1).localScale = new Vector3(scale, scale, scale);
    }
    #endregion
}
