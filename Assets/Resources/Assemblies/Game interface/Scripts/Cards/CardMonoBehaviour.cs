using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardMonoBehaviour : MonoBehaviour {
    private struct Matrix3x3 {
        public float m00, m01, m02,
                     m10, m11, m12,
                     m20, m21, m22;


        public Matrix3x3(
            float m00, float m01, float m02,
            float m10, float m11, float m12,
            float m20, float m21, float m22
        ) {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
        }
        public static Vector3 operator *(Matrix3x3 m, Vector3 v) =>
            new Vector3(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z
            );
        public static Matrix3x3 operator *(float x, Matrix3x3 m) =>
            new Matrix3x3(
                x * m.m00, x * m.m01, x * m.m02,
                x * m.m10, x * m.m11, x * m.m12,
                x * m.m20, x * m.m21, x * m.m22
            );
        public Matrix3x3 inverse() {
            float det(float a, float b, float c, float d) => a * d - b * c;
            float detM =
                  m00 * det(m11, m12, m21, m22)
                - m01 * det(m10, m12, m20, m22)
                + m02 * det(m10, m11, m20, m21);
            Matrix3x3 adjugate = new Matrix3x3(
                +det(m11, m12, m21, m22), -det(m01, m02, m21, m22), +det(m01, m02, m11, m12),
                -det(m10, m12, m20, m22), +det(m00, m02, m20, m22), -det(m00, m02, m10, m12),
                +det(m10, m11, m20, m21), -det(m00, m01, m20, m21), +det(m00, m01, m10, m11)
            );
            return (1 / detM) * adjugate;
        }
    }
    private const int FRAMES_FOR_CARD_FLIP = 60;

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
            0.25f * FRAMES_FOR_CARD_FLIP
        );
        for (int i = 1; i <= FRAMES_FOR_CARD_FLIP; i++) {
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
        for (int i = 1; i <= FRAMES_FOR_CARD_FLIP; i++) {
            transform.localRotation = Quaternion.Slerp(
                startingRotation,
                Quaternion.identity,
                (float)i / FRAMES_FOR_CARD_FLIP
            );
            if (!backToggled && facingCamera(cameraTransform)) {
                toggleBack(false);
                backToggled = true;
            }
            yield return null;
        }
    }
    private float getLinearValue(float start, float end, float x) {
        return (x * (end - start) / FRAMES_FOR_CARD_FLIP) + start;
    }
    private float getQuadraticValue(Vector3 v, float x) {
        return v.x * Mathf.Pow(x, 2)
            + v.y * x
            + v.z;
    }
    private Vector3 getQuadraticCoefs(float yStart, float yMid, float yEnd, float xMid) {
        Matrix3x3 mat = new Matrix3x3(
            0f, 0f, 1f,
            Mathf.Pow(FRAMES_FOR_CARD_FLIP, 2), FRAMES_FOR_CARD_FLIP, 1f,
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
