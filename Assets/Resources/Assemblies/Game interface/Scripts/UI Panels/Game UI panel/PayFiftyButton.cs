using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayFiftyButton : MonoBehaviour {
    public void buttonClicked() {
        DataUIPipelineEventHub.Instance.call_MoneyAdjustment(
            GameState.game.TurnPlayer,
            -GameConstants.PRICE_FOR_LEAVING_JAIL
        );
        DataUIPipelineEventHub.Instance.call_LeaveJail();
    }
}
