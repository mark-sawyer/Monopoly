using UnityEngine;
using UnityEngine.TextCore;

[CreateAssetMenu(menuName = "State/PlayerLandedOnSpaceState")]
public class PlayerLandedOnSpaceState : State {
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        SpaceInfo spaceInfo = GameState.game.SpaceInfoOfTurnPlayer;
        if (spaceInfo is IncomeTaxSpaceInfo) return allStates.getState<IncomeTaxState>();
        if (spaceInfo is GoToJailSpaceInfo) return allStates.getState<PoliceAnimationState>();
        if (spaceInfo is CardSpaceInfo) return allStates.getState<DrawCardState>();
        if (spaceInfo is LuxuryTaxSpaceInfo) return allStates.getState<LuxuryTaxState>();
        if (spaceInfo is PropertySpaceInfo propertySpaceInfo) {
            PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
            if (!propertyInfo.IsBought) return allStates.getState<BuyPropertyOptionState>();
            else if (propertyInfo.Owner != GameState.game.TurnPlayer) return allStates.getState<PayRentState>();
        }
        return allStates.getState<UpdateTurnPlayerState>();
    }
}
