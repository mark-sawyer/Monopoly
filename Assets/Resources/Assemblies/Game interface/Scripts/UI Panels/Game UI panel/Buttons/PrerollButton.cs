using UnityEngine;
using UnityEngine.UI;

public class PrerollButton : MonoBehaviour {
    [SerializeField] private Button button;
    private bool isRotating;



    #region MonoBehaviour
    private void OnEnable() {
        isRotating = false;
        UIEventHub.Instance.sub_PrerollStateStarting(prerollStart);
        UIEventHub.Instance.sub_PrerollStateEnding(prerollEnd);
        CameraEventHub.Instance.sub_RotationStarted(rotationStart);
        CameraEventHub.Instance.sub_RotationFinished(rotationEnd);
        UIPipelineEventHub.Instance.sub_LeaveJail(turnOffWhileLeavingJailListener);
        UIPipelineEventHub.Instance.sub_UseGOOJFCardButtonClicked(turnOffWhileLeavingJailListener);
        prerollStart();
    }
    private void OnDisable() {
        UIEventHub.Instance.unsub_PrerollStateStarting(prerollStart);
        UIEventHub.Instance.unsub_PrerollStateEnding(prerollEnd);
        CameraEventHub.Instance.unsub_RotationStarted(rotationStart);
        CameraEventHub.Instance.unsub_RotationFinished(rotationEnd);
        UIPipelineEventHub.Instance.unsub_LeaveJail(turnOffWhileLeavingJailListener);
        UIPipelineEventHub.Instance.unsub_UseGOOJFCardButtonClicked(turnOffWhileLeavingJailListener);
    }
    #endregion



    #region protected
    protected virtual bool Interactable => true;
    #endregion



    #region private
    private bool RotatingOrAboutTo {
        get {
            CameraController cameraController = CameraController.Instance;
            if (cameraController == null) {
                return false;
            }
            else {
                return isRotating ||
                    (CameraController.Instance.AutoOn && CameraController.Instance.NeedsToRotate);
            }
        }
    }
    private void prerollStart() {
        if (!RotatingOrAboutTo) {
            button.interactable = Interactable;
        }
        else {
            button.interactable = false;
        }
    }
    private void prerollEnd() {
        button.interactable = false;
    }
    private void rotationStart() {
        isRotating = true;
        button.interactable = false;
    }
    private void rotationEnd() {
        isRotating = false;
        button.interactable = Interactable;
    }
    private void turnOffWhileLeavingJailListener() {
        turnOffWhileLeavingJail();
    }
    private void turnOffWhileLeavingJailListener(CardType ct) {
        turnOffWhileLeavingJail();
    }
    private void turnOffWhileLeavingJail() {
        button.interactable = false;
        bool turnPlayerIncurredJailDebt = GameState.game.TurnPlayer.ToMoveAfterJailDebtResolving;
        if (!turnPlayerIncurredJailDebt) {
            WaitFrames.Instance.beforeAction(
                FrameConstants.WAIT_FOR_LEAVING_JAIL,
                () => button.interactable = Interactable
            );
        }
    }
    #endregion
}
