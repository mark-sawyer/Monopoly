using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCover : MonoBehaviour {
    [SerializeField] private Image image;



    #region MonoBehaviour
    private void Start() {
        UIEventHub uiEvents = UIEventHub.Instance;
        uiEvents.sub_FadeScreenCoverIn(startFadeIn);
        uiEvents.sub_FadeScreenCoverOut(startFadeOut);
    }
    #endregion



    #region private
    private void startFadeIn(float goalAlpha) {
        StopAllCoroutines();
        StartCoroutine(fadeIn(goalAlpha));
    }
    private void startFadeOut() {
        StopAllCoroutines();
        StartCoroutine(fadeOut());
    }
    private IEnumerator fadeIn(float goalAlpha) {
        int frames = FrameConstants.SCREEN_COVER_TRANSITION;
        Color colour = image.color;
        float currentAlpha = colour.a;
        Func<float, float> getAlpha = LinearValue.getFunc(currentAlpha, goalAlpha, frames);

        for (int i = 1; i <= frames; i++) {
            float alpha = getAlpha(i);
            colour.a = alpha;
            image.color = colour;
            yield return null;
        }

        colour.a = goalAlpha;
        image.color = colour;
    }
    private IEnumerator fadeOut() {
        float currentAlpha = image.color.a * 255f;
        float slope = -currentAlpha / FrameConstants.SCREEN_COVER_TRANSITION;
        for (int i = 1; i <= FrameConstants.SCREEN_COVER_TRANSITION; i++) {
            float alpha = (currentAlpha + slope * i) / 255f;
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
    #endregion
}
