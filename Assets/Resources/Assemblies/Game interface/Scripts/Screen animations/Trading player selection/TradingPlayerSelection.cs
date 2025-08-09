using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingPlayerSelection : ScreenAnimation {
    #region Internal references
    [SerializeField] private RectTransform droppingQuestionRT;
    [SerializeField] private TradingPlayerSelectionRow tradingCharacterSelectionRow;
    [SerializeField] private TradingTokenReceiver leftTokenReceiver;
    [SerializeField] private TradingTokenReceiver rightTokenReceiver;
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private GameObject confirmGameObject;
    [SerializeField] private Button backButton;
    #endregion
    [SerializeField] private GameObject tradePanelPrefab;
    private GameObject tradePanelInstance;
    private DroppingQuestionsFunctionality droppingQuestionsFunctionality;



    #region MonoBehaviour
    private void OnEnable() {
        droppingQuestionsFunctionality = new DroppingQuestionsFunctionality(droppingQuestionRT);
        droppingQuestionsFunctionality.adjustSize();
        UIEventHub.Instance.sub_TradingPlayerPlaced(adjustTextButtonToggle);
        UIEventHub.Instance.sub_TradingPlayersConfirmed(createTradePanel);
        UIPipelineEventHub.Instance.sub_TradeTerminated(removeTradeDisplay);
        UIPipelineEventHub.Instance.sub_TradeLockedIn(removeTradeDisplay);
    }
    private void OnDestroy() {
        UIEventHub.Instance.unsub_TradingPlayerPlaced(adjustTextButtonToggle);
        UIEventHub.Instance.unsub_TradingPlayersConfirmed(createTradePanel);
        UIPipelineEventHub.Instance.unsub_TradeTerminated(removeTradeDisplay);
        UIPipelineEventHub.Instance.unsub_TradeLockedIn(removeTradeDisplay);
    }
    #endregion



    #region ScreenAnimation
    public override void appear() {
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        tradingCharacterSelectionRow.displayCharacters(activePlayers);
        StartCoroutine(droppingQuestionsFunctionality.drop());
        StartCoroutine(dropBackButton());
    }
    #endregion



    #region private
    private void adjustTextButtonToggle() {
        bool leftActive = leftTokenReceiver.PlayerInfo != null;
        bool rightActive = rightTokenReceiver.PlayerInfo != null;
        bool bothActive = leftActive && rightActive;
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
        GameObject createTradePanel(PlayerInfo playerOne, PlayerInfo playerTwo) {
            GameObject tradePanelInstance = Instantiate(tradePanelPrefab, transform);
            TradePanel tradePanel = tradePanelInstance.GetComponent<TradePanel>();
            tradePanel.setup(playerOne, playerTwo);
            return tradePanelInstance;
        }
        void moveObjects() {
            Destroy(droppingQuestionRT.gameObject);
            RectTransform tradePanelRT = (RectTransform)tradePanelInstance.transform;
            DroppingQuestionsFunctionality dqf = new DroppingQuestionsFunctionality(tradePanelRT);
            dqf.adjustSize();
            IEnumerator dropSequence() {
                yield return dqf.drop();
                backButton.interactable = true;
            }
            StartCoroutine(dropSequence());
        }

        PlayerInfo playerOne = leftTokenReceiver.PlayerInfo;
        PlayerInfo playerTwo = rightTokenReceiver.PlayerInfo;
        DataEventHub.Instance.call_TradeCommenced(playerOne, playerTwo);
        tradePanelInstance = createTradePanel(playerOne, playerTwo);
        moveObjects();
        UIEventHub.Instance.call_FadeScreenCoverIn(1f);
        backButton.interactable = false;
    }
    private IEnumerator dropBackButton() {
        RectTransform backButtonRT = (RectTransform)backButton.transform;
        int frames = FrameConstants.SCREEN_COVER_TRANSITION;
        float x = backButtonRT.anchoredPosition.x;
        float yStart = backButtonRT.anchoredPosition.y;
        float yEnd = -20;
        Func<float, float> getY = LinearValue.getFunc(yStart, yEnd, frames);
        for (int i = 1; i <= frames; i++) {
            float y = getY(i);
            backButtonRT.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
        backButtonRT.anchoredPosition = new Vector2(x, yEnd);
        backButton.interactable = true;
    }
    private void removeTradeDisplay() {
        ScreenAnimationEventHub.Instance.call_RemoveScreenAnimation();
    }
    #endregion
}
