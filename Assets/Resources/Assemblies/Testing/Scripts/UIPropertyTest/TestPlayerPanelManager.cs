using UnityEngine;

public class TestPlayerPanelManager : MonoBehaviour {
    [SerializeField] private PlayerPanel playerPanel;
    [SerializeField] private GameEvent<PlayerInfo> moneyAdjust;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPropertyAdjustmentUI;

    private void Start() {
        moneyAdjust.Listeners += adjustMoney;
        playerPropertyAdjustmentUI.Listeners += adjustPropertyIcons;
    }

    private void adjustMoney(PlayerInfo playerInfo) {
        playerPanel.adjustMoney(playerInfo);
    }
    private void adjustPropertyIcons(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerPanel.updatePropertyIconVisual(playerInfo, propertyInfo);
    }
}
