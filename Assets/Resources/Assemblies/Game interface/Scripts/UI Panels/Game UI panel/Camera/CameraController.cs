using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    private Action cameraUpdate;
    private float anglePerFrame;
    private const int TURN_FRAMES = 40;



    #region Singleton boilerplate
    public static CameraController Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        CameraEventHub.Instance.sub_RotationFinished(rotationFinished);
        CameraEventHub.Instance.sub_ClockwiseTurnClicked(() => setManualRotation(90f));
        CameraEventHub.Instance.sub_CounterClockwiseTurnClicked(() => setManualRotation(-90f));
        cameraUpdate = nothing;
        anglePerFrame = 90f / TURN_FRAMES;
    }
    private void Update() {
        cameraUpdate();
    }
    #endregion



    #region public
    public void turnOnAutoMode() {
        AutoOn = true;
        setAutoRotation();
        UIEventHub.Instance.sub_PrerollStateStarting(setAutoRotation);
    }
    public void turnOffAutoMode() {
        AutoOn = false;
        UIEventHub.Instance.unsub_PrerollStateStarting(setAutoRotation);
    }
    public bool NeedsToRotate {
        get {
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            SpaceInfo spaceInfo = turnPlayer.SpaceInfo;
            int spaceIndex = spaceInfo.Index;
            Quaternion targetRotation = getGoalQuaternion(spaceIndex);
            Quaternion currentrotation = Camera.main.transform.rotation;
            float currentAngle = Quaternion.Angle(currentrotation, targetRotation);
            return currentAngle > 1f;
        }
    }
    public bool AutoOn { get; private set; }
    #endregion



    #region Camera actions
    private void nothing() { }
    private void rotation(Quaternion startRotation, Quaternion targetRotation, float startAngle) {
        float remaining = Quaternion.Angle(Camera.main.transform.rotation, targetRotation);
        float angleSoFar = startAngle - remaining;
        float lerpProp = (angleSoFar + anglePerFrame) / startAngle;
        if (remaining > 0.001f) {
            Camera.main.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, lerpProp);
        }
        else {
            Camera.main.transform.rotation = targetRotation;
            CameraEventHub.Instance.call_RotationFinished();
        }
    }
    #endregion



    #region private
    private void setManualRotation(float angle) {
        Quaternion startRotation = Camera.main.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, angle);
        float startAngle = Quaternion.Angle(startRotation, targetRotation);
        cameraUpdate = () => rotation(startRotation, targetRotation, startAngle);
    }
    private void setAutoRotation() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        SpaceInfo spaceInfo = turnPlayer.SpaceInfo;
        int spaceIndex = spaceInfo.Index;
        Quaternion targetRotation = getGoalQuaternion(spaceIndex);
        Quaternion currentrotation = Camera.main.transform.rotation;
        float currentAngle = Quaternion.Angle(currentrotation, targetRotation);
        if (currentAngle > 1f) {
            CameraEventHub.Instance.call_RotationStarted();
            cameraUpdate = () => rotation(currentrotation, targetRotation, currentAngle);
        }
    }
    private void rotationFinished() {
        cameraUpdate = nothing;
    }
    private Quaternion getGoalQuaternion(int spaceIndex) {
        int row = spaceIndex / 10;
        if (row == 0) return Quaternion.Euler(0f, 0f, 0f);
        else if (row == 1) return Quaternion.Euler(0f, 0f, -90f);
        else if (row == 2) return Quaternion.Euler(0f, 0f, 180f);
        else return Quaternion.Euler(0f, 0f, 90f);
    }
    #endregion
}
