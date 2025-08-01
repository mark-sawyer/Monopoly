using UnityEngine;

[CreateAssetMenu(menuName = "State/UpdateTurnPlayerState")]
public class UpdateTurnPlayerState : State {
    public override void enterState() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (!diceInfo.RolledDoubles || turnPlayer.InJail) {
            DataEventHub.Instance.call_NextPlayerTurn();
        }
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override void exitState() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        bool turnPlayerInJail = turnPlayer.InJail;
        if (turnPlayerInJail) {
            DataEventHub.Instance.call_IncrementJailTurn();
        }            
    }
    public override State getNextState() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (turnPlayer.InJail) return allStates.getState<JailPreRollState>();
        else return allStates.getState<PreRollState>();
    }
}
