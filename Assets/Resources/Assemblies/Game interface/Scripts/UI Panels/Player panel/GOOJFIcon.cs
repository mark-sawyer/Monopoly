using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GOOJFIcon : MonoBehaviour {
    [SerializeField] private Image image;


    public void enable(bool toggle) {
        if (toggle) StartCoroutine(pulse());
        else StartCoroutine(pulseOff());
        
    }
    private IEnumerator pulse() {
        float getScale(float x) {
            if (x <= 5) return 1f + 0.2f * x;
            else return 2f - (1f / 15f) * (x - 5f);
        }

        image.enabled = true;
        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private IEnumerator pulseOff() {
        float getScale(float x) {
            return 1f + 0.1f * x;
        }

        for (int i = 1; i <= 10; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        image.enabled = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
