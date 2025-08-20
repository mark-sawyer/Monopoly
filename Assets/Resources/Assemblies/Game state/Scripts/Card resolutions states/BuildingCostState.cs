using UnityEngine;

[CreateAssetMenu(menuName = "State/BuildingCostState")]
internal class BuildingCostState : State {
    private bool goToResolveDebt;

    public override void enterState() {
        goToResolveDebt = false;

        BuildingCostCardInfo buildingCostCardInfo = (BuildingCostCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int houseCount = turnPlayer.HousesOwned;
        int hotelCount = turnPlayer.HotelsOwned;
        int houseCost = houseCount * buildingCostCardInfo.HouseCost;
        int hotelCost = hotelCount * buildingCostCardInfo.HotelCost;
        int totalCost = houseCost + hotelCost;

        if (totalCost > 0) {
            DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, GameState.game.BankCreditor, totalCost);
            goToResolveDebt = true;
        }
        DataEventHub.Instance.call_CardResolved();
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        if (goToResolveDebt) return allStates.getState<ResolveDebtState>();
        return allStates.getState<PrerollState>();
    }
}
