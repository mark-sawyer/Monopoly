using System.Linq;
using UnityEngine;

public class DoublesTickboxes : MonoBehaviour {
    [SerializeField] private DoublesTickbox[] doublesTickboxArray;



    #region MonoBehaviour
    private void OnEnable() {
        UIEventHub.Instance.sub_DoublesTickBoxUpdate(checkForDoubles); 
        UIEventHub.Instance.sub_NextPlayerTurn(removeAllTicks);
    }
    private void OnDisable() {
        UIEventHub.Instance.unsub_DoublesTickBoxUpdate(checkForDoubles);
        UIEventHub.Instance.unsub_NextPlayerTurn(removeAllTicks);
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
