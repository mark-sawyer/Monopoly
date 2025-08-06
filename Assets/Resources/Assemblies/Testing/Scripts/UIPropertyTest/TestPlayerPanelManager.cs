using UnityEngine;

public class TestPlayerPanelManager : MonoBehaviour {
    [SerializeField] private PlayerPanel playerPanel;

    private void Start() {
        UIPipelineEventHub.Instance.sub_MoneyAdjustment(adjustMoney);
        UIPipelineEventHub.Instance.sub_PlayerPropertyAdjustment(adjustPropertyIcons);
    }

    private void adjustMoney(PlayerInfo playerInfo) {
        playerPanel.adjustMoney(playerInfo);
    }
    private void adjustPropertyIcons(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerPanel.updatePropertyIconVisual(playerInfo, propertyInfo);
    }
}
