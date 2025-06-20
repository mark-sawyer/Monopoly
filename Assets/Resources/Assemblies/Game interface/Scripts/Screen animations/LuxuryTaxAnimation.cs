using System.Collections;
using UnityEngine;

public class LuxuryTaxAnimation : ScreenAnimation {
    [SerializeField] private RectTransform ringTransform;
    [SerializeField] private RectTransform luxuryTaxTextTransform;
    [SerializeField] private RectTransform pay100TextTransform;
    [SerializeField] private SoundEvent sadSound;
    private float canvasWidth;
    private const int TOTAL_ROTATIONS = 2;
    private const int ANIMATION_FRAMES = 250;
    private const int TEXT_ANIMATION_FRAMES = 200;
    private const float GOAL_MAX_HORIZONTAL_PROPORTION = 1380f / 1920f;



    private void Start() {
        appear();
    }
    public override void appear() {
        sadSound.play();
        canvasWidth = ((RectTransform)transform.parent).rect.width;
        StartCoroutine(rollRingAcross());
        StartCoroutine(growAndShrinkText(luxuryTaxTextTransform));
        WaitFrames.Instance.exe(
            ANIMATION_FRAMES - TEXT_ANIMATION_FRAMES,
            () => StartCoroutine(growAndShrinkText(pay100TextTransform))
        );
        WaitFrames.Instance.exe(
            ANIMATION_FRAMES + 20,
            () => removeScreenAnimation.invoke()
        );
    }
    private IEnumerator rollRingAcross() {
        float ringWidth = ringTransform.rect.width * ringTransform.localScale.x;
        float xStart = ringTransform.anchoredPosition.x;
        float xEnd = canvasWidth + (ringWidth / 2f);
        float totalAngle = -360f * TOTAL_ROTATIONS;
        for (int i = 1; i <= ANIMATION_FRAMES; i++) {
            float xPos = LinearValue.exe(i, xStart, xEnd, ANIMATION_FRAMES);
            float angle = LinearValue.exe(i, 0, totalAngle, ANIMATION_FRAMES);
            ringTransform.anchoredPosition = new Vector3(xPos, 0f, 0f);
            ringTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            yield return null;
        }
        ringTransform.anchoredPosition = new Vector3(xEnd, 0f, 0f);
    }
    private IEnumerator growAndShrinkText(RectTransform rt) {
        rt.localScale = Vector3.zero;
        float baseWidth = rt.rect.width;
        float maxScale = GOAL_MAX_HORIZONTAL_PROPORTION * canvasWidth / baseWidth;
        for (int i = 1; i <= TEXT_ANIMATION_FRAMES; i++) {
            float scale;
            if (i <= TEXT_ANIMATION_FRAMES / 2) scale = 2 * i * maxScale / TEXT_ANIMATION_FRAMES;
            else scale = -2 * i * maxScale / TEXT_ANIMATION_FRAMES + 2 * maxScale;
            rt.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        rt.localScale = Vector3.zero;
    }
}
