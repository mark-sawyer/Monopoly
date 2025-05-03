
public class StateManager {
    private State currentGameState;
    private PreRollState preRollState;
    private RollAnimationState rollAnimationState;
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
        preRollState = new PreRollState(r.getRollButton());
        rollAnimationState = new RollAnimationState(r.getDieVisual(), r.getTokenVisuals(), gamePlayer);
        moveTokenState = new MoveTokenState(r.getBoardTransform(), r.getTokenVisuals(), gamePlayer);
        resolveTurnState = new ResolveTurnState(gamePlayer);
    }
    private void assignPossibleNextStates() {
        preRollState.assignPossibleNextStates(new State[] { rollAnimationState });
        rollAnimationState.assignPossibleNextStates(new State[] { moveTokenState });
        moveTokenState.assignPossibleNextStates(new State[] { resolveTurnState });
        resolveTurnState.assignPossibleNextStates(new State[] { preRollState });
    }
}
