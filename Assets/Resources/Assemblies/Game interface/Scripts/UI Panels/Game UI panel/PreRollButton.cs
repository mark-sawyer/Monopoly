using UnityEngine;
using UnityEngine.UI;

public class PrerollButton : MonoBehaviour {
    [SerializeField] private Button button;
    private bool isRotating;



    #region MonoBehaviour
    private void Start() {
        isRotating = false;
        UIEventHub.Instance.sub_PrerollStateStarting(prerollStartListening);
        UIEventHub.Instance.sub_PrerollStateEnding(prerollEndListening);
        CameraEventHub.Instance.sub_RotationStarted(rotationStartListening);
        CameraEventHub.Instance.sub_RotationFinished(rotationEndListening);
        UIPipelineEventHub.Instance.sub_LeaveJail(turnOffWhileLeavingJail);
        UIPipelineEventHub.Instance.sub_UseGOOJFCardButtonClicked((CardType ct) => turnOffWhileLeavingJail());
    }
    #endregion



    #region private
    private void prerollStartListening() {
        if (!rotatingOrAboutTo()) {
            button.interactable = true;
        }
    }
    private void prerollEndListening() {
        button.interactable = false;
    }
    private void rotationStartListening() {
        isRotating = true;
        button.interactable = false;
    }
    private void rotationEndListening() {
        isRotating = false;
        button.interactable = true;
    }
    private void turnOffWhileLeavingJail() {
        button.interactable = false;
        WaitFrames.Instance.beforeAction(
            FrameConstants.WAIT_FOR_LEAVING_JAIL,
            () => { button.interactable = true; }
        );
    }
    private bool rotatingOrAboutTo() {
        return isRotating ||
            (CameraController.Instance.AutoOn && CameraController.Instance.NeedsToRotate);
    }
    #endregion
}
