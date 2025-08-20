using System.Linq;
using UnityEngine;

public class DoublesTickboxes : MonoBehaviour {
    [SerializeField] private DoublesTickbox[] doublesTickboxArray;



    #region MonoBehaviour
    private void OnEnable() {
        UIEventHub.Instance.sub_DoublesTickBoxUpdate(checkForDoubles);
        UIPipelineEventHub.Instance.sub_NextPlayerTurn(removeAllTicks);
    }
    private void OnDisable() {
        UIEventHub.Instance.unsub_DoublesTickBoxUpdate(checkForDoubles);
        UIPipelineEventHub.Instance.unsub_NextPlayerTurn(removeAllTicks);
    }
    #endregion



    #region private
    private void checkForDoubles() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        if (diceInfo.RolledDoubles) {
            addTickToNextTickbox();
            int doublesCount = diceInfo.DoublesInARow;
            if (doublesCount < 3) {
                SoundOnlyEventHub.Instance.call_DoublesDing(doublesCount);
            }
        }
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
