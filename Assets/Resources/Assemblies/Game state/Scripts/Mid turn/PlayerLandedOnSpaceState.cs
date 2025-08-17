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
            int cost = propertyInfo.Cost;
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            int turnPlayerMoney = turnPlayer.Money;
            bool canAfford = cost <= turnPlayerMoney;
            bool unbought = !propertyInfo.IsBought;
            bool isMortgaged = propertyInfo.IsMortgaged;

            if (unbought && canAfford) return allStates.getState<BuyPropertyOptionState>();
            if (unbought) return allStates.getState<UnaffordablePropertyState>();
            if (propertyInfo.Owner == GameState.game.TurnPlayer) return allStates.getState<PrerollState>();
            if (isMortgaged) return allStates.getState<PrerollState>();
            return allStates.getState<PayRentState>();
        }
        return allStates.getState<PrerollState>();
    }
}
