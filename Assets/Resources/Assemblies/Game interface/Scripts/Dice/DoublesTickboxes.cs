using System.Linq;
using UnityEngine;

public class DoublesTickboxes : MonoBehaviour {
    [SerializeField] private DoublesTickbox[] doublesTickboxArray;
    [SerializeField] private GameEvent diceAnimationOver;
    [SerializeField] private GameEvent turnOver;



    private void Start() {
        diceAnimationOver.Listeners += checkForDoubles;
        turnOver.Listeners += removeAllTicks;
    }



    private void checkForDoubles() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        if (diceInfo.RolledDoubles) addTickToNextTickbox();
    }
    private void addTickToNextTickbox() {
        DoublesTickbox doublesTickbox = doublesTickboxArray.First(x => !x.IsTicked);
        doublesTickbox.startAppearTick();
    }
    private void removeAllTicks() {
        if (GameState.game.DiceInfo.RolledDoubles) return;

        foreach (DoublesTickbox dt in doublesTickboxArray) {
            dt.removeTick();
        }
    }
}
