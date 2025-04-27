
public class StateManager {
    private State currentGameState;
    private PreRollState preRollState;
    private MoveTokenState moveTokenState;
    private ResolveTurnState resolveTurnState;

    public StateManager(GamePlayer gamePlayer, ReferencePasser referencePasser) {
        instantiateGameStates(referencePasser, gamePlayer);
        assignPossibleNextStates();
        currentGameState = preRollState;
    }
    public void update() {
        currentGameState.update();
        if (currentGameState.exitConditionMet()) {
            currentGameState.exitState();
            currentGameState = currentGameState.getNextState();
            currentGameState.enterState();
        }
    }



    private void instantiateGameStates(ReferencePasser r, GamePlayer gamePlayer) {
        preRollState = new PreRollState(gamePlayer, r.getRollButton());
        moveTokenState = new MoveTokenState(r.getTokensTransform(), r.getBoardTransform());
        resolveTurnState = new ResolveTurnState();
    }
    private void assignPossibleNextStates() {
        preRollState.assignPossibleNextStates(new State[] { moveTokenState });
        moveTokenState.assignPossibleNextStates(new State[] { resolveTurnState });
        resolveTurnState.assignPossibleNextStates(new State[] { preRollState });
    }
}
