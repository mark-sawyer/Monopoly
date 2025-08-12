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
    public IEnumerator fadeAway() {
        Func<float, float> getAlpha = LinearValue.getFunc(1, 0, FrameConstants.DYING_PLAYER);
        Color colour = image.color;
        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            colour.a = getAlpha(i);
            image.color = colour;
            yield return null;
        }
        image.enabled = false;
    }
    #endregion



    #region private
    private IEnumerator pulse() {
        float getScale(float x) {
            Func<float, float> getEarlyX = LinearValue.getFunc(0f, 5f, 1f, 2f);
            Func<float, float> getLateX = LinearValue.getFunc(5f, 20f, 2f, 1f);

            if (x <= 5) return getEarlyX(x);
            else return getLateX(x);
        }


        SoundOnlyEventHub.Instance.call_AppearingPop();
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
        float getScale(float x) {
            return 1f + 0.1f * x;
        }


        SoundOnlyEventHub.Instance.call_AppearingPop();
        yield return WaitFrames.Instance.frames(10);
        for (int i = 1; i <= 10; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        image.enabled = false;
    }
    #endregion
}
