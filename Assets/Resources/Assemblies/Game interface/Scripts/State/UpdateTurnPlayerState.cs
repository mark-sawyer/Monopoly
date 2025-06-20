using UnityEngine;

[CreateAssetMenu(menuName = "State/UpdateTurnPlayerState")]
public class UpdateTurnPlayerState : State {
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private GameEvent samePlayerTurn;

    public override void enterState() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (diceInfo.RolledDoubles && !turnPlayer.InJail) samePlayerTurn.invoke();
        else nextPlayerTurn.invoke();
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (turnPlayer.InJail) return allStates.getState<JailPreRollState>();
        else return allStates.getState<PreRollState>();
    }
}
