using UnityEngine;

[CreateAssetMenu(menuName = "State/MoveTokenState")]
internal class MoveTokenState : State {
    private bool tokenSettled;



    #region GameState
    public override void enterState() {
        int startingIndex = GameState.game.TurnPlayer.SpaceIndex;
        int diceValues = GameState.game.DiceInfo.TotalValue;
        DataUIPipelineEventHub.Instance.call_TurnPlayerMovedAlongBoard(startingIndex, diceValues);
        tokenSettled = false;
        UIEventHub.Instance.sub_TokenSettled(heardTokenSettle);
    }
    public override bool exitConditionMet() {
        return tokenSettled;
    }
    public override void exitState() {
        UIEventHub.Instance.unsub_TokenSettled(heardTokenSettle);
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

            if (unbought && canAfford) return allStates.getState<BuyPropertyOptionState>();
            else if (unbought) return allStates.getState<UnaffordablePropertyState>();
            else if (propertyInfo.Owner != GameState.game.TurnPlayer) return allStates.getState<PayRentState>();
        }
        return allStates.getState<PrerollState>();
    }
    #endregion



    #region private
    private void heardTokenSettle() {
        tokenSettled = true;
    }
    #endregion
}
