using System;
using UnityEngine;

public class HandshakeInstructions : MonoBehaviour {
    [SerializeField] private GameObject[] instructions;



    #region MonoBehaviour
    private void Awake() {
        UIPipelineEventHub.Instance.sub_TradeUpdated(turnInstructionsOff);
        TradeEventHub.Instance.sub_TradeConditionsMet(turnInstructionsOn);
    }
    private void OnDestroy() {
        UIPipelineEventHub.Instance.unsub_TradeUpdated(turnInstructionsOff);
        TradeEventHub.Instance.unsub_TradeConditionsMet(turnInstructionsOn);
    }
    #endregion



    #region private
    private void turnInstructionsOn() {
        foreach (GameObject instruction in instructions) {
            instruction.SetActive(true);
        }
    }
    private void turnInstructionsOff() {
        foreach (GameObject instruction in instructions) {
            instruction.SetActive(false);
        }
    }
    #endregion
}
