using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnaffordableProperty : ScreenAnimation<PropertyInfo> {
    [SerializeField] private RectTransform textRT;
    [SerializeField] private DeedSpawner deedSpawner;



    #region ScreenAnimation
    public override void appear() {
        UIEventHub.Instance.call_IncorrectOutcome();
        StartCoroutine(deedSpawner.moveDeed());
        StartCoroutine(shakeText());
    }
    public override void setup(PropertyInfo propertyInfo) {
        deedSpawner.spawnDeed(propertyInfo);
    }
    #endregion



    private IEnumerator shakeText() {
        int frames = InterfaceConstants.FRAMES_FOR_SCREEN_COVER_TRANSITION;
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
        WaitFrames.Instance.exe(
            50,
            () => {
                transferDeed();
                ScreenAnimationEventHub.Instance.call_RemoveScreenAnimationKeepCover();
            }
        );
    }
    private void transferDeed() {
        Transform deedSpawnerTransform = deedSpawner.transform;
        Transform deedTransform = deedSpawnerTransform.GetChild(0);
        AuctionManager auctionManager = AuctionManager.Instance;
        auctionManager.takeInVisualChild(deedTransform);
    }
}
