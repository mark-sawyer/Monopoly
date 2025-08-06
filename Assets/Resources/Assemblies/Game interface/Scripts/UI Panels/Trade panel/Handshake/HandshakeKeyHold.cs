using TMPro;
using UnityEngine;

public class HandshakeKeyHold : MonoBehaviour {
    [SerializeField] private KeyCode heldKey;
    [SerializeField] private TextMeshProUGUI textMesh;
    private bool activated;
    private const float TRANSPARENT_ALPHA = 100f / 255f;



    #region MonoBehaviour
    private void OnEnable() {
        activated = false;
        textMesh.color = new Color(1f, 1f, 1f, TRANSPARENT_ALPHA);
    }
    private void Update() {
        if (activated) checkKeyUp();
        else checkKeyDown();
    }
    #endregion



    #region private
    private void checkKeyDown() {
        if (Input.GetKey(heldKey)) {
            activated = true;
            textMesh.color = Color.white;
        }
    }
    private void checkKeyUp() {
        if (!Input.GetKey(heldKey)) {
            activated = false;
            textMesh.color = new Color(1f, 1f, 1f, TRANSPARENT_ALPHA);
        }
    }
    #endregion
}
