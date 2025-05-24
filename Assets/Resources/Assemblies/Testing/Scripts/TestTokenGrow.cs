using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTokenGrow : MonoBehaviour {
    [SerializeField] private float targetScale;
    private const int FRAMES_FOR_TOKEN_GROWING = 50;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(changeScale());
        }
    }

    private IEnumerator changeScale() {
        float startScale = transform.localScale.x;
        int frames = FRAMES_FOR_TOKEN_GROWING;
        float slope = (targetScale - startScale) / frames;
        for (int i = 0; i < frames; i++) {
            float scale = startScale + slope*i;
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }
}
