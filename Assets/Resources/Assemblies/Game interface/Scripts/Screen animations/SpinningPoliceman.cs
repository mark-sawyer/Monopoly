using System.Collections;
using UnityEngine;

public class SpinningPoliceman : ScreenAnimation {
    [SerializeField] private AudioClip whistle;
    private const float FINAL_ANGLE = -30f;
    private const float FINAL_SCALE = 10f;
    private const int TOTAL_ROTATIONS = 5;

    public override void appear() {
        StartCoroutine(STOP());
    }
    private IEnumerator STOP() {
        float timePassed = 0;
        float soundLength = whistle.length;
        float totalAngle = FINAL_ANGLE - 360f * TOTAL_ROTATIONS;
        float spinningTime = 0.8f * soundLength;
        transform.localScale = Vector3.zero;
        while (timePassed < spinningTime) {
            timePassed += Time.deltaTime;
            float angle = totalAngle * timePassed / spinningTime;
            float scale = FINAL_SCALE * timePassed / spinningTime;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, FINAL_ANGLE));
        transform.localScale = new Vector3(FINAL_SCALE, FINAL_SCALE, FINAL_SCALE);
        while (timePassed < soundLength * 2f) {
            timePassed += Time.deltaTime;
            transform.localPosition = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                0f
            );
            yield return null;
        }
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation();
    }
}
