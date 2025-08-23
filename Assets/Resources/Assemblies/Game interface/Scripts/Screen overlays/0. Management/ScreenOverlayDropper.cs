using System;
using System.Collections;
using UnityEngine;

public class ScreenOverlayDropper {
    private RectTransform rt;



    #region public
    public ScreenOverlayDropper(RectTransform rt) {
        this.rt = rt;
        RectAnchorPivotMover rectAnchorPivotMover = new RectAnchorPivotMover(rt);
        rectAnchorPivotMover.moveAnchors(new Vector2(0.5f, 0.5f));
        rectAnchorPivotMover.movePivot(new Vector2(0.5f, 0.5f));
        adjustSize();
    }
    public IEnumerator drop() {
        float start = rt.anchoredPosition.y;
        Func<float, float> getY = LinearValue.getFunc(start, 0, FrameConstants.SCREEN_COVER_TRANSITION);
        for (int i = 1; i <= FrameConstants.SCREEN_COVER_TRANSITION; i++) {
            float y = getY(i);
            Vector2 newPos = new Vector2(0f, y);
            rt.anchoredPosition = newPos;
            yield return null;
        }
        rt.anchoredPosition = Vector2.zero;
    }
    #endregion



    #region private
    private void adjustSize() {
        float canvasWidth = ((RectTransform)rt.parent).rect.width;
        float canvasHeight = ((RectTransform)rt.parent).rect.height;
        float thisWidth = rt.rect.width;
        float thisHeight = rt.rect.height;
        if (thisWidth < canvasWidth && thisHeight < canvasHeight) return;
        float widthAdj = canvasWidth / thisWidth;
        float heightAdj = canvasHeight / thisHeight;
        float finalAdj = widthAdj < heightAdj ? widthAdj : heightAdj;
        rt.localScale = new Vector3(finalAdj, finalAdj, finalAdj);
    }
    #endregion
}
