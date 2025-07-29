using System.Collections.Generic;
using UnityEngine;

public class TradingPlayerSelection : ScreenAnimation {
    [SerializeField] private RectTransform rt;
    [SerializeField] private TradingPlayerSelectionPanel tradingCharacterSelectionPanel;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;



    #region MonoBehaviour
    private void OnEnable() {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality(rt);
        droppingQuestionsFunctionality.adjustSize();
    }
    #endregion



    #region ScreenAnimation
    public override void appear() {
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        tradingCharacterSelectionPanel.displayCharacters(activePlayers);
        StartCoroutine(droppingQuestionsFunctionality.drop());
    }
    #endregion
}
