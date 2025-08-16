using System;
using UnityEngine;
using UnityEngine.UI;

public class HandshakeButtonRevealer : MonoBehaviour {
    [SerializeField] private Button leftAgreeButton;
    [SerializeField] private Button rightAgreeButton;
    [SerializeField] private GameObject[] handshakeButtons;
    [SerializeField] private GameObject holdKeysInstructions;
    [SerializeField] private GameObject clickButtonsInstructions;
    private Action updateAction;
    private bool buttonsShowing;



    #region MonoBehaviour
    private void Awake() {
        UIPipelineEventHub.Instance.sub_TradeUpdated(turnOffKeyListening);
        TradeEventHub.Instance.sub_TradeConditionsMet(turnOnKeyListening);
    }
    private void OnEnable() {
        updateAction = doNothing;
        buttonsShowing = false;
    }
    private void OnDestroy() {
        UIPipelineEventHub.Instance.unsub_TradeUpdated(turnOffKeyListening);
        TradeEventHub.Instance.unsub_TradeConditionsMet(turnOnKeyListening);
    }
    private void Update() {
        updateAction();
    }
    #endregion



    #region private
    private bool HoldingHandshakeKeys {
        get {
            bool properInput = Input.GetKey(KeyCode.Z)
                && Input.GetKey(KeyCode.Q)
                && Input.GetKey(KeyCode.Slash)
                && Input.GetKey(KeyCode.RightBracket);
            bool secretInput = GameState.game.IsTestGame ? Input.GetKey(KeyCode.RightControl) : false;
            return properInput || secretInput;
        }
    }
    private void doNothing() { } 
    private void listenForKeys() {
        if (buttonsShowing && !HoldingHandshakeKeys) toggleHandshakeButtons(false);
        else if (!buttonsShowing && HoldingHandshakeKeys) toggleHandshakeButtons(true);
    }
    private void turnOnKeyListening() {
        updateAction = listenForKeys;
        buttonsShowing = false;
    }
    private void turnOffKeyListening() {
        updateAction = doNothing;
        toggleHandshakeButtons(false);
    }
    private void toggleHandshakeButtons(bool toggle) {
        buttonsShowing = toggle;
        if (toggle) {
            for (int i = 0; i < 3; i++) {
                bool startsInteractable = i == 0;
                GameObject buttonGameObject = handshakeButtons[i];
                buttonGameObject.SetActive(true);
                Button button = buttonGameObject.GetComponent<Button>();
                button.interactable = startsInteractable;
                holdKeysInstructions.SetActive(false);
                clickButtonsInstructions.SetActive(true);
            }
        }
        else {
            Array.ForEach(handshakeButtons, x => x.SetActive(false));
            holdKeysInstructions.SetActive(true);
            clickButtonsInstructions.SetActive(false);
        }
    }
    #endregion
}
