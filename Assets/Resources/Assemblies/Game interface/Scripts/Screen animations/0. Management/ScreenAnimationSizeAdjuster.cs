using UnityEngine;

public class ScreenAnimationSizeAdjuster {
    private RectTransform rt;
    private float scale;



    public float Scale => scale;
    public ScreenAnimationSizeAdjuster(float goalWidthProportion, float defaultWidth, RectTransform rt) {
        this.rt = rt;
        float canvasWidth = ((RectTransform)rt.parent).rect.width;
        scale = goalWidthProportion * canvasWidth / defaultWidth;
    }
    public GameObject InstantiateAdjusted(GameObject prefab) {
        GameObject instance = GameObject.Instantiate(prefab, rt);
        instance.transform.localScale = new Vector3(scale, scale, scale);
        return instance;
    }
    public void adjustChildrenSize() {
        for (int i = 0; i < rt.childCount; i++) {
            Transform transform = rt.GetChild(i);
            float currentScale = transform.localScale.x;
            float newScale = currentScale * scale;
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
