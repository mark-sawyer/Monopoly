using UnityEngine;
using UnityEngine.UI;

public class ResolveDebtTradeButton : MonoBehaviour {
    [SerializeField] private Button button;
    private PlayerInfo debtor;



    #region MonoBehaviour
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(checkInteractability);
    }
    #endregion



    #region public
    public void setup(PlayerInfo debtor) {
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(checkInteractability);
        this.debtor = debtor;
    }
    #endregion



    #region private
    private void checkInteractability() {
        button.interactable = debtor.DebtInfo != null;
    }
    #endregion
}
