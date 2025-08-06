using UnityEngine;
using UnityEngine.UI;

public class AgreeButton : MonoBehaviour {
    [SerializeField] private Button otherButton;
    [SerializeField] private Button button;
    private UIPipelineEventHub uiPipelineEventHub;
    private TradeEventHub tradeEventHub;


    #region MonoBehaviour
    private void Start() {
        uiPipelineEventHub = UIPipelineEventHub.Instance;
        tradeEventHub = TradeEventHub.Instance;
        uiPipelineEventHub.sub_TradeUpdated(adjustInteractability);
    }
    private void OnDestroy() {
        uiPipelineEventHub.unsub_TradeUpdated(adjustInteractability);
    }
    #endregion



    #region public
    public void agreeClicked() {
        button.interactable = false;
        if (!otherButton.interactable) {
            tradeEventHub.call_TradeConditionsMet();
        }
    }
    #endregion



    #region private
    private void adjustInteractability() {
        button.interactable = !GameState.game.TradeIsEmpty;
    }
    #endregion
}
