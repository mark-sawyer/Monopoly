using System.Collections;
using UnityEngine;

public class ScreenOverlayDropper {
    private RectTransform rt;



    public ScreenOverlayDropper(RectTransform rt) {
        this.rt = rt;
    }
    public void adjustSize() {
        float canvasWidth = ((RectTransform)rt.parent).rect.width;
        float canvasHeight = ((RectTransform)rt.parent).rect.height;
        float thisWidth =  rt.rect.width;
        float thisHeight = rt.rect.height;
        if (thisWidth < canvasWidth && thisHeight < canvasHeight) return;
        float widthAdj = canvasWidth / thisWidth;
        float heightAdj = canvasHeight / thisHeight;
        float finalAdj = widthAdj < heightAdj ? widthAdj : heightAdj;
        rt.localScale = new Vector3(finalAdj, finalAdj, finalAdj);
    }
    public IEnumerator drop() {
        Vector2 start = rt.anchoredPosition;
        for (int i = 1; i <= FrameConstants.SCREEN_COVER_TRANSITION; i++) {
            Vector2 newPos = start - (i * start / FrameConstants.SCREEN_COVER_TRANSITION);
            rt.anchoredPosition = newPos;
            yield return null;
        }
    }
}
