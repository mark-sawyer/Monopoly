using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardMonoBehaviour : MonoBehaviour {
    public void startCoroutines(Transform cameraTransform) {
        StartCoroutine(position());
        StartCoroutine(rotation(cameraTransform));
    }
    private IEnumerator position() {
        float xStart = transform.localPosition.x;
        float yStart = transform.localPosition.y;
        Quaternion startingRotation = transform.localRotation;
        Vector3 quadraticCoefs = getQuadraticCoefs(
            yStart,
            0.5f * yStart,
            0f,
            0.25f * InterfaceConstants.FRAMES_FOR_CARD_FLIP
        );
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_CARD_FLIP; i++) {
            transform.localPosition = new Vector3(
                getLinearValue(xStart, 0f, i),
                getQuadraticValue(quadraticCoefs, i),
                0f
            );
            yield return null;
        }
        transform.localPosition = new Vector3();
    }
    private IEnumerator rotation(Transform cameraTransform) {
        Quaternion startingRotation = transform.localRotation;
        bool backToggled = false;
        for (int i = 1; i <= InterfaceConstants.FRAMES_FOR_CARD_FLIP; i++) {
            transform.localRotation = Quaternion.Slerp(
                startingRotation,
                Quaternion.identity,
                (float)i / InterfaceConstants.FRAMES_FOR_CARD_FLIP
            );
            if (!backToggled && facingCamera(cameraTransform)) {
                toggleBack(false);
                backToggled = true;
            }
            yield return null;
        }
    }
    private float getLinearValue(float start, float end, float x) {
        return (x * (end - start) / InterfaceConstants.FRAMES_FOR_CARD_FLIP) + start;
    }
    private float getQuadraticValue(Vector3 v, float x) {
        return v.x * Mathf.Pow(x, 2)
            + v.y * x
            + v.z;
    }
    private Vector3 getQuadraticCoefs(float yStart, float yMid, float yEnd, float xMid) {
        Matrix3x3 mat = new Matrix3x3(
            0f, 0f, 1f,
            Mathf.Pow(InterfaceConstants.FRAMES_FOR_CARD_FLIP, 2), InterfaceConstants.FRAMES_FOR_CARD_FLIP, 1f,
            Mathf.Pow(xMid, 2), xMid, 1
        );
        Matrix3x3 inv = mat.inverse();
        Vector3 v = new Vector3(yStart, yEnd, yMid);
        return inv * v;
    }
    private void toggleBack(bool toggle) {
        void togglePanel(Transform panel, bool toggle) {
            Transform sections = panel.GetChild(0);
            for (int i = 0; i < 9; i++) {
                Transform child = sections.GetChild(i);
                Image image = child.GetComponent<Image>();
                image.enabled = toggle;
            }
        }
        Transform backOuterPanel = transform.GetChild(1);
        Transform backInnerPanel = backOuterPanel.GetChild(1);
        togglePanel(backOuterPanel, toggle);
        togglePanel(backInnerPanel, toggle);
    }
    private bool facingCamera(Transform cameraTransform) {
        return Vector3.Dot(transform.forward, cameraTransform.forward) > 0;
    }
}
