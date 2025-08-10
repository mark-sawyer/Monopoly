using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnaffordableProperty : ScreenOverlay<PropertyInfo> {
    [SerializeField] private RectTransform textRT;



    #region ScreenAnimation
    public override void appear() {
        SoundOnlyEventHub.Instance.call_IncorrectOutcome();
        StartCoroutine(shakeText());
    }
    public override void setup(PropertyInfo propertyInfo) {
        AccompanyingVisualSpawner.Instance.spawnAndMove(textRT, propertyInfo);
    }
    #endregion



    private IEnumerator shakeText() {
        int frames = FrameConstants.SCREEN_COVER_TRANSITION;
        Func<float, float> firstHalf = LinearValue.getFunc(5f, -5f, frames / 4f);
        Func<float, float> secondHalf = LinearValue.getFunc(frames / 4f, frames / 2f, -5f, 5f);
        float fullFunc(float x) {
            if (x <= frames / 4f) return firstHalf(x);
            else return secondHalf(x);
        }
        LinearValue.getFunc(frames / 2f, frames, -5f, 5f);
        for (int i = 1; i <= 2 * frames; i++) {
            int x = i % (frames / 2);
            float zRot = fullFunc(x);
            Vector3 euler = new Vector3(0f, 0f, zRot);
            textRT.localRotation = Quaternion.Euler(euler);
            yield return null;
        }
        WaitFrames.Instance.beforeAction(
            50,
            () => ScreenOverlayEventHub.Instance.call_RemoveScreenAnimationKeepCover()
        );
    }
}
