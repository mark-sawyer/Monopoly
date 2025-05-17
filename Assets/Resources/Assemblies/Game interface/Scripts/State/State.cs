using UnityEngine;

public abstract class State : ScriptableObject {
    [SerializeField] protected State[] possibleNextStates;

    public virtual void enterState() { }
    public virtual void update() { }
    public abstract bool exitConditionMet();
    public virtual void exitState() { }  // For actions required before going to the next state, not for setting up for next time.
    public abstract State getNextState();
    public State getStateType<T>() {
        for (int i = 0; i < possibleNextStates.Length; i++) {
            if (possibleNextStates[i] is T) return possibleNextStates[i];
        }
        throw new System.Exception("State not found.");
    }
}
