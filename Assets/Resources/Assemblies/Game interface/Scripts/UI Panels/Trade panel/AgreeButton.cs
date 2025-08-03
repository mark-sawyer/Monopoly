using UnityEngine;
using UnityEngine.UI;

public class AgreeButton : MonoBehaviour {
    [SerializeField] private Button button;
    private UIEventHub uiEventHub;


    #region MonoBehaviour
    private void Start() {
        uiEventHub = UIEventHub.Instance;
        uiEventHub.sub_TradeUpdated(adjustInteractability);
    }
    private void OnDestroy() {
        uiEventHub.unsub_TradeUpdated(adjustInteractability);
    }
    #endregion



    #region public
    public void agreeClicked() {
        button.interactable = false;
    }
    #endregion



    #region private
    private void adjustInteractability() {
        button.interactable = !GameState.game.TradeIsEmpty;
    }
    #endregion
}
