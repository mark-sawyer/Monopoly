using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardMonoBehaviour : MonoBehaviour {
    [SerializeField] private GameObject backGameObject;



    #region public
    public void startCoroutines() {
        StartCoroutine(position());
        StartCoroutine(rotation());
        WaitFrames.Instance.beforeAction(
            FrameConstants.CARD_FLIP - 10,
            UIEventHub.Instance.call_CardDrop
        );
        WaitFrames.Instance.beforeAction(
            FrameConstants.CARD_FLIP,
            () => StartCoroutine(tinyShake())
        );
    }
    public void turnOffBack() {
        backGameObject.SetActive(false);
    }
    #endregion



    #region Coroutines
    private IEnumerator position() {
        float xStart = transform.localPosition.x;
        float yStart = transform.localPosition.y;
        Func<float, float> getX = LinearValue.getFunc(xStart, 0f, FrameConstants.CARD_FLIP);
        Quaternion startingRotation = transform.localRotation;
        Vector3 quadraticCoefs = getQuadraticCoefs(
            yStart,
            0.5f * yStart,
            0f,
            0.25f * FrameConstants.CARD_FLIP
        );
        for (int i = 1; i <= FrameConstants.CARD_FLIP; i++) {
            transform.localPosition = new Vector3(
                getX(i),
                getQuadraticValue(quadraticCoefs, i),
                0f
            );
            yield return null;
        }
        transform.localPosition = new Vector3();
    }
    private IEnumerator rotation() {
        Quaternion startingRotation = transform.localRotation;
        for (int i = 1; i <= FrameConstants.CARD_FLIP; i++) {
            transform.localRotation = Quaternion.Slerp(
                startingRotation,
                Quaternion.identity,
                (float)i / FrameConstants.CARD_FLIP
            );
            if (backGameObject.activeSelf && facingCamera(Camera.main.transform)) {
                backGameObject.SetActive(false);
            }
            yield return null;
        }
    }
    private IEnumerator tinyShake() {
        float range = 3;
        for (int i = 0; i < 5; i++) {
            transform.localPosition = new Vector3(
                UnityEngine.Random.Range(-range, range),
                UnityEngine.Random.Range(-range, range),
                0f
            );
            yield return null;
        }
    }
    #endregion



    #region private
    private float getQuadraticValue(Vector3 v, float x) {
        return v.x * Mathf.Pow(x, 2)
            + v.y * x
            + v.z;
    }
    private Vector3 getQuadraticCoefs(float yStart, float yMid, float yEnd, float xMid) {
        Matrix3x3 mat = new Matrix3x3(
            0f, 0f, 1f,
            Mathf.Pow(FrameConstants.CARD_FLIP, 2), FrameConstants.CARD_FLIP, 1f,
            Mathf.Pow(xMid, 2), xMid, 1
        );
        Matrix3x3 inv = mat.inverse();
        Vector3 v = new Vector3(yStart, yEnd, yMid);
        return inv * v;
    }
    private bool facingCamera(Transform cameraTransform) {
        return Vector3.Dot(transform.forward, cameraTransform.forward) > 0;
    }
    #endregion
}
