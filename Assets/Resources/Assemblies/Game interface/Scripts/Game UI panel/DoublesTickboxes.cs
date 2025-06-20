using System.Linq;
using UnityEngine;

public class DoublesTickboxes : MonoBehaviour {
    [SerializeField] private DoublesTickbox[] doublesTickboxArray;
    [SerializeField] private GameEvent diceAnimationOver;
    [SerializeField] private GameEvent nextPlayerTurnUI;



    #region MonoBehaviour
    private void OnEnable() {
        diceAnimationOver.Listeners += checkForDoubles;
        nextPlayerTurnUI.Listeners += removeAllTicks;
    }
    private void OnDisable() {
        diceAnimationOver.Listeners -= checkForDoubles;
        nextPlayerTurnUI.Listeners -= removeAllTicks;
    }
    #endregion



    #region private
    private void checkForDoubles() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        if (diceInfo.RolledDoubles) addTickToNextTickbox();
    }
    private void addTickToNextTickbox() {
        DoublesTickbox doublesTickbox = doublesTickboxArray.First(x => !x.IsTicked);
        doublesTickbox.startAppearTick();
    }
    private void removeAllTicks() {
        foreach (DoublesTickbox dt in doublesTickboxArray) {
            dt.removeTick();
        }
    }
    #endregion
}
