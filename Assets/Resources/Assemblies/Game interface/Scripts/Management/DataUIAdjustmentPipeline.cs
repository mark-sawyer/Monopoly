using UnityEngine;

public class DataUIAdjustmentPipeline : MonoBehaviour {
    [SerializeField] private PlayerIntEvent moneyAdjustment;
    [SerializeField] private PlayerIntEvent moneyAdjustmentData;
    [SerializeField] private PlayerEvent moneyAdjustmentUI;
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;
    [SerializeField] private PlayerPropertyEvent playerObtainedPropertyData;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustmentUI;
    [SerializeField] private GameEvent turnPlayer;
    [SerializeField] private GameEvent turnPlayerData;
    [SerializeField] private GameEvent turnPlayerUI;

    private void Start() {
        moneyAdjustment.Listeners += moneyPipeline;
        playerObtainedProperty.Listeners += propertyBoughtPipeline;
        turnPlayer.Listeners += turnPlayerPipeline;
    }
    private void moneyPipeline(PlayerInfo playerInfo, int adjustment) {
        moneyAdjustmentData.invoke(playerInfo, adjustment);
        moneyAdjustmentUI.invoke(playerInfo);
    }
    private void propertyBoughtPipeline(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        playerObtainedPropertyData.invoke(playerInfo, propertyInfo);
        playerPropertyAdjustmentUI.invoke(playerInfo, propertyInfo);
    }
    private void turnPlayerPipeline() {
        turnPlayerData.invoke();
        turnPlayerUI.invoke();
    }
}
