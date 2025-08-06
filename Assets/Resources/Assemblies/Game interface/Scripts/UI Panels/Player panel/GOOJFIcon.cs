using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GOOJFIcon : MonoBehaviour {
    [SerializeField] private Image image;
    private UIEventHub uiEventHub;



    #region MonoBehaviour
    private void Start() {
        uiEventHub = UIEventHub.Instance;
    }
    #endregion



    #region public
    public bool IsOn => image.enabled;
    public IEnumerator enable(bool toggle) {
        if (toggle) yield return pulse();
        else yield return pulseOff();        
    }
    #endregion



    #region private
    private IEnumerator pulse() {
        float getScale(float x) {
            if (x <= 5) return 1f + 0.2f * x;
            else return 2f - (1f / 15f) * (x - 5f);
        }

        uiEventHub.call_AppearingPop();
        yield return WaitFrames.Instance.frames(5);
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

        uiEventHub.call_AppearingPop();
        for (int i = 1; i <= 10; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        image.enabled = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion
}
