using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ScreenCover : MonoBehaviour {
    [SerializeField] private Image image;
    private const float maxAlpha = 220f;



    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(fadeIn());
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(fadeOut());
        }
    }
    


    private IEnumerator fadeIn() {
        float slope = maxAlpha / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            float alpha = (slope * i) / 255f;
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
    private IEnumerator fadeOut() {
        float slope = -maxAlpha / InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_QUESTION_ON_SCREEN_TRANSITION; i++) {
            float alpha = (maxAlpha + slope * i) / 255f;
            image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
}
