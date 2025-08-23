using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaiseMoneyTradeSelection : MonoBehaviour {
    #region Internal references
    [SerializeField] private TradingPlayerSelectionRow tradingPlayerSelectionRow;
    [SerializeField] private TokenIcon leftTokenIcon;
    [SerializeField] private RaiseMoneyTradingTokenReceiver rightTokenReceiver;
    [SerializeField] private RectTransform droppingQuestionRT;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private GameObject confirmGameObject;
    #endregion
    [SerializeField] private GameObject tradePanelPrefab;
    private ResolveDebtPanel resolveDebtPanel;
    private GameObject tradePanelInstance;
    private PlayerInfo debtor;



    #region MonoBehaviour
    private void OnEnable() {
        TradeEventHub.Instance.sub_TradingPlayerPlaced(adjustTextButtonToggle);
        TradeEventHub.Instance.sub_TradingPlayersConfirmed(createTradePanel);
        UIPipelineEventHub.Instance.sub_TradeTerminated(tradeTerminated);
        UIPipelineEventHub.Instance.sub_TradeLockedIn(tradeComplete);
    }
    private void OnDestroy() {
        TradeEventHub.Instance.unsub_TradingPlayerPlaced(adjustTextButtonToggle);
        TradeEventHub.Instance.unsub_TradingPlayersConfirmed(createTradePanel);
        UIPipelineEventHub.Instance.unsub_TradeTerminated(tradeTerminated);
        UIPipelineEventHub.Instance.unsub_TradeLockedIn(tradeComplete);
    }
    #endregion



    #region public
    public void setupAndAppear(PlayerInfo debtor, ResolveDebtPanel resolveDebtPanel) {
        this.debtor = debtor;
        this.resolveDebtPanel = resolveDebtPanel;

        leftTokenIcon.setup(debtor.Token, debtor.Colour);

        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        IEnumerable<PlayerInfo> activePlayersWithoutDebtor = activePlayers.Where(x => !x.Equals(debtor));
        tradingPlayerSelectionRow.displayCharacters(activePlayersWithoutDebtor);

        ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(droppingQuestionRT);
        StartCoroutine(screenOverlayDropper.drop());
        StartCoroutine(dropBackButton());
        SoundPlayer.Instance.play_OtherChime();
    }
    #endregion



    #region private
    private void adjustTextButtonToggle() {
        bool rightActive = rightTokenReceiver.PlayerInfo != null;
        if (rightActive) {
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
            ScreenOverlayDropper screenOverlayDropper = new ScreenOverlayDropper(tradePanelRT);
            IEnumerator dropSequence() {
                yield return screenOverlayDropper.drop();
                backButton.interactable = true;
            }
            StartCoroutine(dropSequence());
        }

        PlayerInfo playerOne = debtor;
        PlayerInfo playerTwo = rightTokenReceiver.PlayerInfo;
        DataEventHub.Instance.call_TradeCommenced(playerOne, playerTwo);
        tradePanelInstance = createTradePanel(playerOne, playerTwo);
        moveObjects();
        UIEventHub.Instance.call_FadeScreenCoverIn(1f);
        SoundPlayer.Instance.play_TradeChime();
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
    private void tradeTerminated() {
        resolveDebtPanel.appearFromTradeBack();
        Destroy(gameObject);
    }
    private void tradeComplete() {
        resolveDebtPanel.appearFromTradeComplete();
        Destroy(gameObject);
    }
    #endregion
}
