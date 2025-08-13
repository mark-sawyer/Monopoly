using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/CameraEventHub")]
public class CameraEventHub : ScriptableObject {
    private static CameraEventHub instance;
    [SerializeField] private GameEvent clockwiseTurnClicked;
    [SerializeField] private GameEvent counterClockwiseTurnClicked;
    [SerializeField] private GameEvent autoCameraButtonClicked;
    [SerializeField] private GameEvent rotationStarted;
    [SerializeField] private GameEvent rotationFinished;



    #region public
    public static CameraEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<CameraEventHub>(
                    "ScriptableObjects/Events/0. Hubs/camera_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_ClockwiseTurnClicked() => clockwiseTurnClicked.invoke();
    public void call_CounterClockwiseTurnClicked() => counterClockwiseTurnClicked.invoke();
    public void call_AutoCameraButtonClicked() => autoCameraButtonClicked.invoke();
    public void call_RotationStarted() => rotationStarted.invoke();
    public void call_RotationFinished() => rotationFinished.invoke();
    #endregion



    #region Subscribing
    public void sub_ClockwiseTurnClicked(Action a) => clockwiseTurnClicked.Listeners += a;
    public void sub_CounterClockwiseTurnClicked(Action a) => counterClockwiseTurnClicked.Listeners += a;
    public void sub_AutoCameraButtonClicked(Action a) => autoCameraButtonClicked.Listeners += a;
    public void sub_RotationStarted(Action a) => rotationStarted.Listeners += a;
    public void sub_RotationFinished(Action a) => rotationFinished.Listeners += a;
    #endregion
}
