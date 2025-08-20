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
    [SerializeField] private GameEvent put;
    [SerializeField] private GameEvent take;
    [SerializeField] private GameEvent swoop;
    [SerializeField] private GameEvent otherChime;
    [SerializeField] private GameEvent flourish;
    [SerializeField] private IntEvent risingBom;
    [SerializeField] private GameEvent brickLaying;
    [SerializeField] private GameEvent dub;
    [SerializeField] private GameEvent buildingPut;
    [SerializeField] private GameEvent wipeSound;
    [SerializeField] private IntEvent doublesDing;



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
    public void call_Put() => put.invoke();
    public void call_Take() => take.invoke();
    public void call_Swoop() => swoop.invoke();
    public void call_OtherChime() => otherChime.invoke();
    public void call_Flourish() => flourish.invoke();
    public void call_RisingBom(int i) => risingBom.invoke(i);
    public void call_BrickLaying() => brickLaying.invoke();
    public void call_Dub() => dub.invoke();
    public void call_BuildingPut() => buildingPut.invoke();
    public void call_WipeSound() => wipeSound.invoke();
    public void call_DoublesDing(int count) => doublesDing.invoke(count);
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
    public void sub_Put(Action a) => put.Listeners += a;
    public void sub_Take(Action a) => take.Listeners += a;
    public void sub_Swoop(Action a) => swoop.Listeners += a;
    public void sub_OtherChime(Action a) => otherChime.Listeners += a;
    public void sub_Flourish(Action a) => flourish.Listeners += a;
    public void sub_RisingBom(Action<int> a) => risingBom.Listeners += a;
    public void sub_BrickLaying(Action a) => brickLaying.Listeners += a;
    public void sub_Dub(Action a) => dub.Listeners += a;
    public void sub_BuildingPut(Action a) => buildingPut.Listeners += a;
    public void sub_WipeSound(Action a) => wipeSound.Listeners += a;
    public void sub_DoublesDing(Action<int> a) => doublesDing.Listeners += a;
    #endregion
}
