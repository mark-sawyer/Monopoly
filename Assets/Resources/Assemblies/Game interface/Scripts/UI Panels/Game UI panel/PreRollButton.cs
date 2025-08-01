using UnityEngine;
using UnityEngine.UI;

public class PreRollButton : MonoBehaviour {
    [SerializeField] private Button button;

    private void Start() {
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(() => button.interactable = false);
        ScreenAnimationEventHub.Instance.sub_TradeOpened(() => button.interactable = false);
        UIEventHub uiEvents = UIEventHub.Instance;
        uiEvents.sub_RollButtonClicked(() => button.interactable = false);
        uiEvents.sub_PayFiftyButtonClicked(() => button.interactable = false);
        uiEvents.sub_UseGOOJFCardButtonClicked((CardType ct) => button.interactable = false);

        uiEvents.sub_PreRollStateStarting(() => button.interactable = true);
        uiEvents.sub_JailPreRollStateStarting(() => button.interactable = true);
    }
}
