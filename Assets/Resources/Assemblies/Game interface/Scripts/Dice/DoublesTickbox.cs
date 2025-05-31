using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoublesTickbox : MonoBehaviour {
    [SerializeField] private Image tickImage;
    private const int APPEAR_FRAMES = 30;
    private bool isTicked;



    #region public
    public bool IsTicked => isTicked;
    public void startAppearTick() {
        isTicked = true;
        tickImage.enabled = true;
        StartCoroutine(appearTick());
    }
    public void removeTick() {
        isTicked = false;
        tickImage.enabled = false;
    }
    #endregion



    #region private
    private IEnumerator appearTick() {
        Transform tickTransform = tickImage.transform;
        for (int i = 1; i <= APPEAR_FRAMES; i++) {
            float alpha = i * 1f / APPEAR_FRAMES;
            float size = 3f - i * 2f / APPEAR_FRAMES;
            tickImage.color = new Color(1f, 1f, 1f, alpha);
            tickTransform.localScale = new Vector3(size, size, size);
            yield return null;
        }
        tickImage.color = new Color(1f, 1f, 1f, 1f);
        tickTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion
}
