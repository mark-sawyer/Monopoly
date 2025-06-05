using UnityEngine;

[CreateAssetMenu(menuName = "State/PoliceAnimationState")]
public class PoliceAnimationState : State {
    [SerializeField] private GameEvent spinningPoliceman;
    [SerializeField] private GameEvent policemanAnimationOver;
    private bool animationOver;



    #region GameState
    public override void enterState() {
        spinningPoliceman.invoke();
        animationOver = false;
        policemanAnimationOver.Listeners += animationOverCalled;
    }
    public override bool exitConditionMet() {
        return animationOver;
    }

    public override State getNextState() {
        return getState<MoveTokenToJailState>();
    }
    public override void exitState() {
        policemanAnimationOver.Listeners -= animationOverCalled;
    }
    #endregion



    private void animationOverCalled() {
        animationOver = true;
    }
}
