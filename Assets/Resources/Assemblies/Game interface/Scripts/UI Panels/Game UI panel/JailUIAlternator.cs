using UnityEngine;

public class JailUIAlternator : MonoBehaviour {
    [SerializeField] private GameObject doublesGameObject;
    [SerializeField] private GameObject jailOptionsGameObject;



    #region MonoBehaviour
    private void Start() {
        UIEventHub uiEvents = UIEventHub.Instance;
        UIPipelineEventHub uiPipelineEvents = UIPipelineEventHub.Instance;

        uiEvents.sub_PrerollStateStarting(setMode);
        uiPipelineEvents.sub_LeaveJail(setMode);
        uiPipelineEvents.sub_UseGOOJFCardButtonClicked((CardType ct) => setMode());
    }
    #endregion



    #region private
    private void setMode() {
        if (GameState.game.TurnPlayer.InJail) {
            doublesGameObject.SetActive(false);
            jailOptionsGameObject.SetActive(true);
        }
        else {
            doublesGameObject.SetActive(true);
            jailOptionsGameObject.SetActive(false);
        }
    }
    #endregion
}
