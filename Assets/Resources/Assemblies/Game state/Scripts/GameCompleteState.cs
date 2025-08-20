using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "State/GameCompleteState")]
internal class GameCompleteState : State {
    public override void enterState() {
        PlayerInfo winner = GameState.game.ActivePlayers.ElementAt(0);
        ScreenOverlayEventHub.Instance.call_WinnerAnnounced(winner);
    }
    public override bool exitConditionMet() {
        return Input.GetKeyDown(KeyCode.Escape);
    }
    public override void exitState() {
        Application.Quit();
    }
    public override State getNextState() {
        throw new System.NotImplementedException();
    }
}
