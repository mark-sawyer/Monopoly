using UnityEngine;

public class RectAnchorPivotMover {
    private RectTransform rt;



    public RectAnchorPivotMover(RectTransform rt) {
        this.rt = rt;
    }
    public void movePivot(Vector2 newPivot) {
        Vector2 startPivot = rt.pivot;
        Vector2 size = rt.rect.size * rt.localScale;
        Vector2 constant = rt.anchoredPosition - size * startPivot;
        Vector2 newPosition = size * newPivot + constant;
        rt.pivot = newPivot;
        rt.anchoredPosition = newPosition;
    }
    public void moveAnchors(Vector2 newAnchor) {
        // Assumes we're moving something with a point anchor, not a stretch.

        RectTransform parent = (RectTransform)rt.parent;
        Vector2 parentSize = parent.rect.size;
        Vector2 oldAnchor = rt.anchorMin;
        Vector2 delta = oldAnchor * parentSize - newAnchor * parentSize;
        rt.anchorMin = newAnchor;
        rt.anchorMax = newAnchor;
        Vector2 newPosition = rt.anchoredPosition + delta;
        rt.anchoredPosition = newPosition;
    }
}
