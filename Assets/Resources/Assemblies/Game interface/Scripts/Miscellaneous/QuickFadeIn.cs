using System;
using System.Collections;
using UnityEngine;

public class QuickFadeIn : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup;
    private int fadeInFrames = 10;



    #region MonoBehaviour
    private void OnEnable() {
        canvasGroup.alpha = 0f;
        StartCoroutine(fadeIn());
    }
    private void OnDisable() {
        StopAllCoroutines();
    }
    #endregion



    #region private
    private IEnumerator fadeIn() {
        Func<float, float> getAlpha = LinearValue.getFunc(0f, 1f, fadeInFrames);

        for (int i = 1; i <= fadeInFrames; i++) {
            float alpha = getAlpha(i);
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }
    #endregion
}
