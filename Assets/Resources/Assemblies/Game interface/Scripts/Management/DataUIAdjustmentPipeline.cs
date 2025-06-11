using UnityEngine;

public class DataUIAdjustmentPipeline : MonoBehaviour {
    [SerializeField] PlayerIntEvent moneyAdjustment;
    [SerializeField] PlayerIntEvent moneyAdjustmentData;
    [SerializeField] PlayerEvent moneyAdjustmentUI;

    private void Start() {
        moneyAdjustment.Listeners += moneyPipeline;
    }
    private void moneyPipeline(PlayerInfo playerInfo, int adjustment) {
        moneyAdjustmentData.invoke(playerInfo, adjustment);
        moneyAdjustmentUI.invoke(playerInfo);
    }
}
