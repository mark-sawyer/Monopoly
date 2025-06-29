
using UnityEngine;

[CreateAssetMenu(menuName = "State/PostManagePropertiesClosedState")]
public class PostManagePropertiesClosedState : State {
    [SerializeField] private GameEvent updateIconsAfterManagePropertiesClosed;
    [SerializeField] private GameEvent iconsUpdatedAfterManagePropertiesClosed;
    private bool updateAnimationsOver;



    #region State
    public override void enterState() {
        iconsUpdatedAfterManagePropertiesClosed.Listeners += updateAnimationsOverListener;
        updateAnimationsOver = false;
        updateIconsAfterManagePropertiesClosed.invoke();
    }
    public override bool exitConditionMet() {
        return updateAnimationsOver;
    }
    public override void exitState() {
        iconsUpdatedAfterManagePropertiesClosed.Listeners -= updateAnimationsOverListener;
    }
    public override State getNextState() {
        if (GameState.game.TurnPlayer.InJail) return allStates.getState<JailPreRollState>();
        else return allStates.getState<PreRollState>();
    }
    #endregion



    #region private
    private void updateAnimationsOverListener() {
        updateAnimationsOver = true;
    }
    #endregion
}
