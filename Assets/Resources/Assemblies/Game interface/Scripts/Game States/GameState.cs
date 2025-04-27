
public abstract class GameState {
    protected Game game;
    protected GameState[] possibleNextState;

    public GameState(Game game) {
        this.game = game;
    }
    public virtual void enterState() { }
    public virtual void update() { }
    public abstract bool exitConditionMet();
    public virtual void exitState() { }
    public abstract GameState getNextState();
    public void assignPossibleNextStates(GameState[] possibleNextState) {
        this.possibleNextState = possibleNextState;
    }
}
