using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCover : MonoBehaviour {
    [SerializeField] private Image image;
    private float storedMaxAlpha;



    public void startFadeIn(float maxAlpha) {
        storedMaxAlpha = maxAlpha;
        StartCoroutine(fadeIn(maxAlpha));
    }
    public void startFadeOut() {
        StartCoroutine(fadeOut());
    }
    private IEnumerator fadeIn(float maxAlpha) {
        float slope = maxAlpha / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            float alpha = (slope * i) / 255f;
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
    private IEnumerator fadeOut() {
        float slope = -storedMaxAlpha / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            float alpha = (storedMaxAlpha + slope * i) / 255f;
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
}
