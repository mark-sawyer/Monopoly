using UnityEngine;
using UnityEngine.UI;

public class PrerollButton : MonoBehaviour {
    [SerializeField] private Button button;



    private void Start() {
        UIEventHub uiEvents = UIEventHub.Instance;
        UIPipelineEventHub uiPipelineEvents = UIPipelineEventHub.Instance;

        uiEvents.sub_PrerollStateStarting(() => button.interactable = true);
        uiEvents.sub_PrerollStateEnding(() => button.interactable = false);
        uiPipelineEvents.sub_LeaveJail(turnOffWhileLeavingJail);
        uiPipelineEvents.sub_UseGOOJFCardButtonClicked((CardType ct) => turnOffWhileLeavingJail());
    }
    private void turnOffWhileLeavingJail() {
        button.interactable = false;
        WaitFrames.Instance.beforeAction(
            FrameConstants.WAIT_FOR_LEAVING_JAIL,
            () => { button.interactable = true; }
        );
    }
}
