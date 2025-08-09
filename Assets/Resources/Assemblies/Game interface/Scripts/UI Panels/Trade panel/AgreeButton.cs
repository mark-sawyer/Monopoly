using UnityEngine;
using UnityEngine.UI;

public class AgreeButton : MonoBehaviour {
    [SerializeField] private Button otherButton;
    [SerializeField] private Button button;


    #region MonoBehaviour
    private void Start() {
        UIPipelineEventHub.Instance.sub_TradeUpdated(adjustInteractability);
    }
    private void OnDestroy() {
        UIPipelineEventHub.Instance.unsub_TradeUpdated(adjustInteractability);
    }
    #endregion



    #region public
    public void agreeClicked() {
        button.interactable = false;
        if (!otherButton.interactable) {
            TradeEventHub.Instance.call_TradeConditionsMet();
        }
    }
    #endregion



    #region private
    private void adjustInteractability() {
        button.interactable = !GameState.game.TradeIsEmpty;
    }
    #endregion
}
