using System.Collections;
using UnityEngine;

public abstract class DroppedQuestion : MonoBehaviour {
    [SerializeField] protected GameEvent questionAnswered;



    #region MonoBehaviour
    private void Start() {
        float canvasWidth = ((RectTransform)transform.parent).rect.width;
        float canvasHeight = ((RectTransform)transform.parent).rect.height;
        float thisWidth = ((RectTransform)transform).rect.width;
        float thisHeight = ((RectTransform)transform).rect.height;
        if (thisWidth < canvasWidth && thisHeight < canvasHeight) return;
        float widthAdj = canvasWidth / thisWidth;
        float heightAdj = canvasHeight / thisHeight;
        float finalAdj = widthAdj < heightAdj ? widthAdj : heightAdj;
        transform.localScale = new Vector3(finalAdj, finalAdj, finalAdj);
    }
    #endregion



    public void drop() {
        StartCoroutine(bringDownQuestion());
    }
    public void disappear() {
        Destroy(gameObject);
    }
    protected virtual void dropComplete() { }
    private IEnumerator bringDownQuestion() {
        RectTransform rt = (RectTransform)transform;
        Vector2 start = rt.anchoredPosition;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            Vector2 newPos = start - (i * start / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION);
            rt.anchoredPosition = newPos;
            yield return null;
        }
        dropComplete();
    }
}
