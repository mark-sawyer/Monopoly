using UnityEngine;

internal abstract class State : ScriptableObject {
    protected static AllStates allStates;

    public static void initialiseAllStates(AllStates allStatesArg) {
        allStates = allStatesArg;
    }
    public virtual void enterState() { }
    public abstract bool exitConditionMet();
    public virtual void exitState() { }  // For actions required before going to the next state, not for setting up for next time.
    public abstract State getNextState();
}
