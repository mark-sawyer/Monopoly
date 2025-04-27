
public abstract class State {
    protected State[] possibleNextStates;

    public virtual void enterState() { }
    public virtual void update() { }
    public abstract bool exitConditionMet();
    public virtual void exitState() { }
    public abstract State getNextState();
    public void assignPossibleNextStates(State[] possibleNextState) {
        this.possibleNextStates = possibleNextState;
    }
}
