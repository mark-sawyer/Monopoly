using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour {
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI onOffText;
    private bool autoOn;



    #region MonoBehaviour
    private void Start() {
        CameraEventHub.Instance.sub_RotationStarted(rotationStarted);
        CameraEventHub.Instance.sub_RotationFinished(rotationFinished);
        CameraEventHub.Instance.sub_AutoCameraButtonClicked(() => toggleAuto(!autoOn));
        CameraEventHub.Instance.sub_ClockwiseTurnClicked(() => toggleAuto(false));
        CameraEventHub.Instance.sub_CounterClockwiseTurnClicked(() => toggleAuto(false));
        toggleAuto(true);
    }
    #endregion



    #region private
    private void toggleAuto(bool toggle) {
        autoOn = toggle;
        if (toggle) {
            UIEventHub.Instance.unsub_PrerollStateStarting(turnOnButton);
            UIEventHub.Instance.unsub_PrerollStateEnding(turnOffButton);
            cameraController.turnOnAutoMode();
            onOffText.text = "ON";
        }
        else {
            UIEventHub.Instance.sub_PrerollStateStarting(turnOnButton);
            UIEventHub.Instance.sub_PrerollStateEnding(turnOffButton);
            cameraController.turnOffAutoMode();
            onOffText.text = "OFF";
        }
    }
    private void turnOnButton() {
        button.interactable = true;
    }
    private void turnOffButton() {
        button.interactable = false;
    }
    private void rotationStarted() {
        button.interactable = false;
    }
    private void rotationFinished() {
        if (!autoOn) {
            button.interactable = true;
        }
    }
    #endregion
}
