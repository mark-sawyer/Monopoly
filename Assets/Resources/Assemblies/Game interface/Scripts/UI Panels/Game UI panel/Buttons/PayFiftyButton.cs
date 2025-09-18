using UnityEngine;

public class PayFiftyButton : PrerollButton {
    protected override bool Interactable {
        get {
            PlayerInfo turnPlayer = GameState.game.TurnPlayer;
            bool canAfford = turnPlayer.Money >= 50;
            bool notTurnThree = turnPlayer.TurnInJail < 3;
            return canAfford && notTurnThree;
        }
    }
    public void buttonClicked() {
        DataUIPipelineEventHub.Instance.call_MoneyAdjustment(
            GameState.game.TurnPlayer,
            -GameConstants.PRICE_FOR_LEAVING_JAIL
        );
        DataUIPipelineEventHub.Instance.call_LeaveJail();
    }
}
