using UnityEngine;

public class GameUIPanelManager : MonoBehaviour {

    void Start() {
        scalePanels();
    }

    void Update() {

    }
    private void scalePanels() {
        Rect panelRect = ((RectTransform)transform).rect;
        Rect canvasRect = ((RectTransform)transform.parent).rect;
        float maxWidthForPanel = (canvasRect.width - canvasRect.height) / 2f;
        float scale = maxWidthForPanel / panelRect.width;
        transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
        transform.GetChild(1).localScale = new Vector3(scale, scale, scale);
    }
}
