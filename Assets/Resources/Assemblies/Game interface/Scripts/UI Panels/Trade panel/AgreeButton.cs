using UnityEngine;
using UnityEngine.UI;

public class AgreeButton : MonoBehaviour {
    [SerializeField] private Button otherButton;
    [SerializeField] private Button button;
    private UIEventHub uiEventHub;
    private TradeEventHub tradeEventHub;


    #region MonoBehaviour
    private void Start() {
        uiEventHub = UIEventHub.Instance;
        tradeEventHub = TradeEventHub.Instance;
        uiEventHub.sub_TradeUpdated(adjustInteractability);
    }
    private void OnDestroy() {
        uiEventHub.unsub_TradeUpdated(adjustInteractability);
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
