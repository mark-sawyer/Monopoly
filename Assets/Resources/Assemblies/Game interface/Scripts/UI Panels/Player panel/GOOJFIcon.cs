using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GOOJFIcon : MonoBehaviour {
    [SerializeField] private Image image;



    #region public
    public bool IsOn => image.enabled;
    public IEnumerator enable(bool toggle) {
        if (toggle) yield return pulse();
        else yield return pulseOff();        
    }
    #endregion



    #region private
    private float getScale(float x) {
        Func<float, float> getEarlyX = LinearValue.getFunc(0f, 5f, 1f, 2f);
        Func<float, float> getLateX = LinearValue.getFunc(5f, 20f, 2f, 1f);

        if (x <= 5) return getEarlyX(x);
        else return getLateX(x);
    }
    private IEnumerator pulse() {
        SoundPlayer.Instance.play_Pop();
        yield return WaitFrames.Instance.frames(10);
        image.enabled = true;
        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private IEnumerator pulseOff() {
        SoundPlayer.Instance.play_Pop();
        yield return WaitFrames.Instance.frames(10);
        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            if (i == 6) image.enabled = false;
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion
}
