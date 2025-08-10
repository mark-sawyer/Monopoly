using UnityEngine;

[CreateAssetMenu(menuName = "State/UpdateTurnPlayerState")]
internal class UpdateTurnPlayerState : State {
    public override void enterState() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (!diceInfo.RolledDoubles || turnPlayer.InJail || !turnPlayer.IsActive) {
            DataUIPipelineEventHub.Instance.call_NextPlayerTurn();
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
        return allStates.getState<PrerollState>();
    }
}
