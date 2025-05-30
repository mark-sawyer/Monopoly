using System.Linq;
using UnityEngine;

public abstract class State : ScriptableObject {
    [SerializeField] private State[] possibleNextStates;

    public virtual void enterState() { }
    public virtual void update() { }
    public abstract bool exitConditionMet();
    public virtual void exitState() { }  // For actions required before going to the next state, not for setting up for next time.
    public abstract State getNextState();
    public State getState<T>() {
        return possibleNextStates.First(x => x is T);
    }
}
