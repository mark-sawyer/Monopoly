using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(menuName = "State/LuxuryTaxState")]
public class LuxuryTaxState : State {
    private bool animationOver;


    #region
    public override void enterState() {
        ScreenAnimationEventHub.Instance.sub_RemoveScreenAnimation(animationOverCalled);
        animationOver = false;
        DataEventHub.Instance.call_PlayerIncurredDebt(
            GameState.game.TurnPlayer,
            GameState.game.BankCreditor,
            GameConstants.LUXURY_TAX
        );
        ScreenAnimationEventHub.Instance.call_LuxuryTaxAnimationBegins();
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        ScreenAnimationEventHub.Instance.unsub_RemoveScreenAnimation(animationOverCalled);
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion


    private void animationOverCalled() {
        animationOver = true;
    }
}
