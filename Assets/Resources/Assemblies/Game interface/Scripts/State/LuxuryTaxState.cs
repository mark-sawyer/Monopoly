using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(menuName = "State/LuxuryTaxState")]
public class LuxuryTaxState : State {
    [SerializeField] private GameEvent luxuryTaxAnimationBegins;
    [SerializeField] private PlayerCreditorIntEvent playerIncurredDebt;
    private bool animationOver;


    #region
    public override void enterState() {
        ScreenAnimation.removeScreenAnimation.Listeners += animationOverCalled;
        animationOver = false;
        playerIncurredDebt.invoke(
            GameState.game.TurnPlayer,
            GameState.game.Bank,
            GameConstants.LUXURY_TAX
        );
        luxuryTaxAnimationBegins.invoke();
    }
    public override bool exitConditionMet() {
        return animationOver;
    }
    public override void exitState() {
        ScreenAnimation.removeScreenAnimation.Listeners -= animationOverCalled;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
    #endregion


    private void animationOverCalled() {
        animationOver = true;
    }
}
