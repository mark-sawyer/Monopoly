using UnityEngine;
using UnityEngine.UI;

public class ButtonInManageProperties : MonoBehaviour {
    [SerializeField] private Button button;
    private bool prewipeStatus;
    private bool prewipeStatusSet;



    #region MonoBehaviour
    private void OnEnable() {
        ManagePropertiesEventHub.Instance.sub_PanelPaused(turnOffForPause);
        ManagePropertiesEventHub.Instance.sub_PanelUnpaused(restoreStatusAfterPause);
        prewipeStatusSet = false;
    }
    private void OnDisable() {
        ManagePropertiesEventHub.Instance.unsub_PanelPaused(turnOffForPause);
        ManagePropertiesEventHub.Instance.unsub_PanelUnpaused(restoreStatusAfterPause);
    }
    #endregion



    #region private
    private void turnOffForPause() {
        prewipeStatus = button.interactable;
        prewipeStatusSet = true;
        button.interactable = false;
    }
    private void restoreStatusAfterPause() {
        if (!prewipeStatusSet) return;

        button.interactable = prewipeStatus;
    }
    #endregion
}
