using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerLandedOnSpaceState")]
internal class PlayerLandedOnSpaceState : State {
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        SpaceInfo spaceInfo = GameState.game.TurnPlayer.SpaceInfo;
        if (spaceInfo is IncomeTaxSpaceInfo) return allStates.getState<IncomeTaxState>();
        if (spaceInfo is GoToJailSpaceInfo) return allStates.getState<MoveTokenToJailState>();
        if (spaceInfo is CardSpaceInfo) return allStates.getState<DrawCardState>();
        if (spaceInfo is LuxuryTaxSpaceInfo) return allStates.getState<LuxuryTaxState>();
        if (spaceInfo is PropertySpaceInfo propertySpaceInfo) {
            PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
            if (!propertyInfo.IsBought) return allStates.getState<BuyPropertyOptionState>();
            else if (propertyInfo.Owner != GameState.game.TurnPlayer) return allStates.getState<PayRentState>();
        }
        return allStates.getState<PrerollState>();
    }
}
