using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerLandedOnSpaceState")]
public class PlayerLandedOnSpaceState : State {
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        SpaceInfo spaceInfo = GameState.game.SpaceInfoOfTurnPlayer;
        if (spaceInfo is IncomeTaxSpaceInfo) return allStates.getState<IncomeTaxState>();
        if (spaceInfo is GoToJailSpaceInfo) return allStates.getState<MoveTokenToJailState>();
        if (spaceInfo is CardSpaceInfo) return allStates.getState<DrawCardState>();
        if (spaceInfo is LuxuryTaxSpaceInfo) return allStates.getState<LuxuryTaxState>();
        if (spaceInfo is PropertySpaceInfo propertySpaceInfo) {
            PropertyInfo propertyInfo = propertySpaceInfo.PropertyInfo;
            int cost = propertyInfo.Cost;
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            int turnPlayerMoney = turnPlayer.Money;
            bool canAfford = cost <= turnPlayerMoney;
            bool unbought = !propertyInfo.IsBought;

            if (unbought && canAfford) return allStates.getState<BuyPropertyOptionState>();
            else if (unbought) return allStates.getState<UnaffordablePropertyState>();
            else if (propertyInfo.Owner != GameState.game.TurnPlayer) return allStates.getState<PayRentState>();
        }
        return allStates.getState<UpdateTurnPlayerState>();
    }
}
