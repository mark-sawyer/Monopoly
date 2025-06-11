using UnityEngine;

[CreateAssetMenu(menuName = "State/UpdateTurnPlayerState")]
public class UpdateTurnPlayerState : State {
    [SerializeField] private GameEvent nextPlayerTurn;
    [SerializeField] private GameEvent samePlayerTurn;

    public override bool exitConditionMet() {
        return true;
    }
    public override void exitState() {
        DiceInfo diceInfo = GameState.game.DiceInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        if (diceInfo.RolledDoubles && !turnPlayer.InJail) samePlayerTurn.invoke();
        else nextPlayerTurn.invoke();
    }
    public override State getNextState() {
        return allStates.getState<PreRollState>();
    }
}
