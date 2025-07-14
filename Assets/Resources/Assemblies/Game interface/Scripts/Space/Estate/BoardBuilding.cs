using System;
using System.Collections;
using UnityEngine;

public class BoardBuilding : MonoBehaviour {
    [SerializeField] private SpriteRenderer spriteRenderer;
    public static readonly int FRAMES = 20;
    private float xPos = -999f;
    private const float HEIGHT = 3f;



    #region public
    public void appear() {
        gameObject.SetActive(true);
        if (xPos == -999f) xPos = transform.localPosition.x;
        transform.localPosition += new Vector3(0f, HEIGHT, 0f);
        StartCoroutine(appearCoroutine());
    }
    public void disappear() {
        StartCoroutine(disappearCoroutine());
    }
    #endregion



    #region private
    private IEnumerator appearCoroutine() {
        Func<float, float> getY = LinearValue.getFunc(HEIGHT, 0f, FRAMES);
        Func<float, float> getAlpha = LinearValue.getFunc(0f, 1f, FRAMES);

        for (int i = 1; i <= FRAMES; i++) {
            transform.localPosition = new Vector3(xPos, getY(i), 0f);
            spriteRenderer.color = new Color(1f, 1f, 1f, getAlpha(i));
            yield return null;
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        transform.localPosition = new Vector3(xPos, 0f, 0f);
    }
    private IEnumerator disappearCoroutine() {
        Func<float, float> getY = LinearValue.getFunc(0f, HEIGHT, FRAMES);
        Func<float, float> getAlpha = LinearValue.getFunc(1f, 0f, FRAMES);

        for (int i = 1; i <= FRAMES; i++) {
            transform.localPosition = new Vector3(xPos, getY(i), 0f);
            spriteRenderer.color = new Color(1f, 1f, 1f, getAlpha(i));
            yield return null;
        }
        gameObject.SetActive(false);
    }
    #endregion
}
