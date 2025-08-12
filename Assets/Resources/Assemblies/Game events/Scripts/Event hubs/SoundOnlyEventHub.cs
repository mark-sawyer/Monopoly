using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Hubs/SoundOnlyEventHub")]
public class SoundOnlyEventHub : ScriptableObject {
    private static SoundOnlyEventHub instance;
    [SerializeField] private GameEvent correctOutcome;
    [SerializeField] private GameEvent incorrectOutcome;
    [SerializeField] private GameEvent buttonDown;
    [SerializeField] private GameEvent buttonUp;
    [SerializeField] private GameEvent dramaticWail;
    [SerializeField] private GameEvent appearingPop;
    [SerializeField] private GameEvent punch;
    [SerializeField] private GameEvent cardDrawn;
    [SerializeField] private GameEvent whistle;



    #region public
    public static SoundOnlyEventHub Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<SoundOnlyEventHub>(
                    "ScriptableObjects/Events/0. Hubs/sound_only_event_hub"
                );
            }
            return instance;
        }
    }
    #endregion



    #region Invoking
    public void call_CorrectOutcome() => correctOutcome.invoke();
    public void call_IncorrectOutcome() => incorrectOutcome.invoke();
    public void call_ButtonDown() => buttonDown.invoke();
    public void call_ButtonUp() => buttonUp.invoke();
    public void call_DramaticWail() => dramaticWail.invoke();
    public void call_AppearingPop() => appearingPop.invoke();
    public void call_Punch() => punch.invoke();
    public void call_CardDrawn() => cardDrawn.invoke();
    public void call_Whistle() => whistle.invoke();
    #endregion


    #region Subscribing
    public void sub_CorrectOutcome(Action a) => correctOutcome.Listeners += a;
    public void sub_IncorrectOutcome(Action a) => incorrectOutcome.Listeners += a;
    public void sub_ButtonDown(Action a) => buttonDown.Listeners += a;
    public void sub_ButtonUp(Action a) => buttonUp.Listeners += a;
    public void sub_DramaticWail(Action a) => dramaticWail.Listeners += a;
    public void sub_AppearingPop(Action a) => appearingPop.Listeners += a;
    public void sub_Punch(Action a) => punch.Listeners += a;
    public void sub_CardDrawn(Action a) => cardDrawn.Listeners += a;
    public void sub_Whistle(Action a) => whistle.Listeners += a;
    #endregion
}
