using UnityEngine;

public class TestPlayerPanelManager : MonoBehaviour {
    [SerializeField] private PlayerPanel playerPanel;

    private void Start() {
        UIEventHub.Instance.sub_MoneyAdjustment(adjustMoney);
        UIEventHub.Instance.sub_PlayerPropertyAdjustment(adjustPropertyIcons);
    }

    private void adjustMoney(PlayerInfo playerInfo) {
        playerPanel.adjustMoney(playerInfo);
    }
    private void adjustPropertyIcons(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerPanel.updatePropertyIconVisual(playerInfo, propertyInfo);
    }
}
