using System.Collections.Generic;
using UnityEngine;

public class TradingPlayerSelection : ScreenAnimation {
    [SerializeField] private RectTransform rt;
    [SerializeField] private TradingPlayerSelectionRow tradingCharacterSelectionRow;
    [SerializeField] private TradingTokenReceiver leftTokenReceiver;
    [SerializeField] private TradingTokenReceiver rightTokenReceiver;
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private GameObject confirmGameObject;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;



    #region MonoBehaviour
    private void OnEnable() {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality(rt);
        droppingQuestionsFunctionality.adjustSize();
        UIEventHub.Instance.sub_TradingPlayerPlaced(adjustTextButtonToggle);
        UIEventHub.Instance.sub_TradingPlayersConfirmed(createTradePanel);
    }
    #endregion



    #region ScreenAnimation
    public override void appear() {
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        tradingCharacterSelectionRow.displayCharacters(activePlayers);
        StartCoroutine(droppingQuestionsFunctionality.drop());
    }
    #endregion



    #region private
    private void adjustTextButtonToggle() {
        bool bothActive = leftTokenReceiver.HasToken && rightTokenReceiver.HasToken;
        if (bothActive) {
            textGameObject.SetActive(false);
            confirmGameObject.SetActive(true);
        }
        else {
            textGameObject.SetActive(true);
            confirmGameObject.SetActive(false);
        }
    }
    private void createTradePanel() {
        PlayerInfo getPlayerInfo(TradingTokenReceiver tradingTokenReceiver) {
            Token token = tradingTokenReceiver.Token;
            PlayerColour colour = tradingTokenReceiver.Colour;
            IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
            foreach (PlayerInfo playerInfo in activePlayers) {
                Token thisToken = playerInfo.Token;
                PlayerColour thisColour = playerInfo.Colour;
                bool match = token == thisToken && colour == thisColour;
                if (match) return playerInfo;
            }
            throw new System.Exception("No matching player found.");
        }

        PlayerInfo playerOne = getPlayerInfo(leftTokenReceiver);
        PlayerInfo playerTwo = getPlayerInfo(rightTokenReceiver);
    }
    #endregion
}
