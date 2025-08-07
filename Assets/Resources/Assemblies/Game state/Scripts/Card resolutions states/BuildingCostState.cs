using UnityEngine;

[CreateAssetMenu(menuName = "State/BuildingCostState")]
internal class BuildingCostState : State {
    public override void enterState() {
        BuildingCostCardInfo buildingCostCardInfo = (BuildingCostCardInfo)GameState.game.DrawnCard.CardMechanicInfo;
        PlayerInfo turnPlayer = GameState.game.TurnPlayer;
        int houseCount = turnPlayer.HousesOwned;
        int hotelCount = turnPlayer.HotelsOwned;
        int houseCost = houseCount * buildingCostCardInfo.HouseCost;
        int hotelCost = hotelCount * buildingCostCardInfo.HotelCost;
        int totalCost = houseCost + hotelCost;
        DataEventHub.Instance.call_PlayerIncurredDebt(GameState.game.TurnPlayer, GameState.game.BankCreditor, totalCost);
        DataEventHub.Instance.call_CardResolved();
    }
    public override bool exitConditionMet() {
        return true;
    }
    public override State getNextState() {
        return allStates.getState<ResolveDebtState>();
    }
}
