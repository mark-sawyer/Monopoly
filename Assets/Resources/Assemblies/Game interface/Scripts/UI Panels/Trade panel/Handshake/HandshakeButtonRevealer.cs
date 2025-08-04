using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandshakeButtonRevealer : MonoBehaviour {
    [SerializeField] private Button leftAgreeButton;
    [SerializeField] private Button rightAgreeButton;
    [SerializeField] private GameObject[] handshakeButtons;
    private Action updateAction;
    private bool buttonsShowing;



    #region MonoBehaviour
    private void OnEnable() {
        UIEventHub.Instance.sub_TradeUpdated(turnOffKeyListening);
        TradeEventHub.Instance.sub_TradeConditionsMet(turnOnKeyListening);
        updateAction = doNothing;
        buttonsShowing = false;
    }
    private void OnDestroy() {
        UIEventHub.Instance.unsub_TradeUpdated(turnOffKeyListening);
        TradeEventHub.Instance.unsub_TradeConditionsMet(turnOnKeyListening);
    }
    private void Update() {
        updateAction();
    }
    #endregion



    #region private
    private bool HoldingHandshakeKeys {
        get {
            //return Input.GetKey(KeyCode.Z)
            //    && Input.GetKey(KeyCode.Q)
            //    && Input.GetKey(KeyCode.Slash)
            //    && Input.GetKey(KeyCode.RightBracket);
            return Input.GetKey(KeyCode.G);
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
            }
        }
        else {
            Array.ForEach(handshakeButtons, x => x.SetActive(false));
        }
    }
    #endregion
}
