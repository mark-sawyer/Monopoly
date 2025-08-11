using UnityEngine;

[CreateAssetMenu(menuName = "State/UpdateTurnPlayerState")]
internal class UpdateTurnPlayerState : State {
    public override void enterState() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (turnPlayer.HasLostTurn || !turnPlayer.IsActive) {
            DataUIPipelineEventHub.Instance.call_NextPlayerTurn();
            PlayerInfo newTurnPlayer = GameState.game.TurnPlayer;
            if (newTurnPlayer.InJail) {
                DataEventHub.Instance.call_IncrementJailTurn();
            }
        }
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        return allStates.getState<PrerollState>();
    }
}
