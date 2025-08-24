using UnityEngine;
using UnityEngine.UI;

public class DeclareBankruptcyButton : MonoBehaviour {
    [SerializeField] private Button button;
    private PlayerInfo debtor;



    #region MonoBehaviour
    private void OnDestroy() {
        ResolveDebtEventHub.Instance.unsub_ResolveDebtVisualRefresh(checkInteractability);
    }
    #endregion



    #region public
    public void setup(PlayerInfo debtor) {
        this.debtor = debtor;
        ResolveDebtEventHub.Instance.sub_ResolveDebtVisualRefresh(checkInteractability);
        checkInteractability();
    }
    #endregion



    #region private
    private void checkInteractability() {
        bool cannotRaiseMoney = !debtor.HasAnUnmortgagedProperty;
        bool debtExists = debtor.DebtInfo != null;
        button.interactable = debtExists && cannotRaiseMoney;
    }
    #endregion
}
