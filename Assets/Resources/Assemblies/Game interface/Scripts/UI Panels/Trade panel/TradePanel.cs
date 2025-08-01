using UnityEngine;

public class TradePanel : MonoBehaviour {
    [SerializeField] private TradePanelSide leftSide;
    [SerializeField] private TradePanelSide rightSide;



    public void setup(PlayerInfo leftPlayerInfo, PlayerInfo rightPlayerInfo) {
        leftSide.setup(leftPlayerInfo);
        rightSide.setup(rightPlayerInfo);
    }
}
