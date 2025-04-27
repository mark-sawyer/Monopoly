using UnityEngine.UI;

public class GameStateManager {
    private Game game;
    private GameState currentGameState;
    private PreRollState preRollState;
    private MoveTokenState moveTokenState;
    private ResolveTurnState resolveTurnState;

    public GameStateManager(Game game, Button rollButton, GetMovingTokenInformation getMovingTokenInformation) {
        this.game = game;
        instantiateGameStates(rollButton, getMovingTokenInformation);
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



    private void instantiateGameStates(Button rollButton, GetMovingTokenInformation getMovingTokenInformation) {
        preRollState = new PreRollState(game, rollButton);
        moveTokenState = new MoveTokenState(game, getMovingTokenInformation);
        resolveTurnState = new ResolveTurnState(game);
    }
    private void assignPossibleNextStates() {
        preRollState.assignPossibleNextStates(new GameState[] { moveTokenState });
        moveTokenState.assignPossibleNextStates(new GameState[] { resolveTurnState });
        resolveTurnState.assignPossibleNextStates(new GameState[] { preRollState });
    }
}
