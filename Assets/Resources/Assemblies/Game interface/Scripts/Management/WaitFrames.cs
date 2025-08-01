using System;
using System.Collections;
using UnityEngine;

public class WaitFrames : MonoBehaviour {
    #region Singleton boilerplate
    public static WaitFrames Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region public
    public void exe(int frames, Action a) {
        StartCoroutine(waitThenAction(frames, a));
    }
    public void exe<T>(int frames, Action<T> a, T arg) {
        StartCoroutine(waitThenAction(frames, a, arg));
    }
    #endregion



    #region private
    private static IEnumerator waitThenAction(int frames, Action a) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke();
    }
    private static IEnumerator waitThenAction<T>(int frames, Action<T> a, T arg) {
        for (int i = 0; i < frames; i++) {
            yield return null;
        }
        a.Invoke(arg);
    }
    #endregion
}
