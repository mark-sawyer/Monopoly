using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PlayerPanelManager : MonoBehaviour {
    #region External references
    [SerializeField] private GameObject playerPanelPrefab;
    #endregion
    private const float GAP = 3;
    private UIEventHub uiEventHub;
    private UIPipelineEventHub uiPipelineEvents;
    private ManagePropertiesEventHub managePropertiesEvents;
    private TradeEventHub tradeEvents;



    #region MonoBehaviour
    private void Start() {
        instantiatePanels();
        scalePanels();
        setupPanels();
        subscribeToEvents();
        getPlayerPanel(0).toggleHighlightImage(true);
    }
    #endregion



    #region Start functions
    private void instantiatePanels() {
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            GameObject newPanel = Instantiate(playerPanelPrefab, transform);
            RectTransform rt = (RectTransform)newPanel.transform;
            float yPos = -GAP - (rt.rect.height + GAP) * i;
            rt.anchoredPosition = new Vector3(0f, yPos);
        }
    }
    private void scalePanels() {
        Rect panelRect = ((RectTransform)transform.GetChild(0)).rect;
        Rect canvasRect = ((RectTransform)transform.parent).rect;

        float panelsHeight = (panelRect.height + GAP) * GameState.game.NumberOfPlayers + GAP;
        float panelsWidth = panelRect.width + (2 * GAP);

        float maxHeightForPlayerPanels = canvasRect.height;
        float maxWidthForPlayerPanels = (canvasRect.width - canvasRect.height) / 2f;

        float scaleForHeight = maxHeightForPlayerPanels / panelsHeight;
        float scaleForWidth = maxWidthForPlayerPanels / panelsWidth;

        float heightIfScaledForWidth = panelsHeight * scaleForWidth;
        float scaleUsed = heightIfScaledForWidth <= canvasRect.height ? scaleForWidth : scaleForHeight;

        transform.localScale = new Vector3(scaleUsed, scaleUsed, scaleUsed);
    }
    private void setupPanels() {
        IEnumerable<PlayerInfo> players = GameState.game.PlayerInfos;
        int i = 0;
        foreach (PlayerInfo player in players) {
            transform.GetChild(i).GetComponent<PlayerPanel>().setup(player);
            i += 1;
        }
    }
    private void subscribeToEvents() {
        uiPipelineEvents = UIPipelineEventHub.Instance;
        uiEventHub = UIEventHub.Instance;
        managePropertiesEvents = ManagePropertiesEventHub.Instance;
        tradeEvents = TradeEventHub.Instance;

        uiPipelineEvents.sub_MoneyAdjustment(adjustMoneyVisual);
        uiPipelineEvents.sub_MoneyBetweenPlayers(adjustMoneyVisuals);
        uiPipelineEvents.sub_PlayerPropertyAdjustment(updatePropertyIcons);
        uiPipelineEvents.sub_NextPlayerTurn(updateTurnPlayerHighlight);
        uiPipelineEvents.sub_PlayerGetsGOOJFCard(updateGOOJFCardIcon);
        uiPipelineEvents.sub_UseGOOJFCardButtonClicked(
            (CardType cardType) => updateGOOJFCardIcon(GameState.game.TurnPlayer, cardType)
        );

        uiEventHub.sub_UpdateUIMoney(adjustMoneyVisuals);

        managePropertiesEvents.sub_ManagePropertiesOpened(() => changeMoneyAdjustListening(true));
        managePropertiesEvents.sub_BackButtonPressed(() => changeMoneyAdjustListening(false));
        managePropertiesEvents.sub_UpdateIconsAfterManagePropertiesClosed(checkForUpdatesAfterBackButtonPushed);

        tradeEvents.sub_UpdateVisualsAfterTradeFinalised(updateVisualsAfterTradeListening);
    }
    #endregion



    #region Listeners
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoney(playerInfo);
    }
    private void adjustMoneyVisuals(PlayerInfo playerOne, PlayerInfo playerTwo) {
        adjustMoneyVisual(playerOne);
        adjustMoneyVisual(playerTwo);
    }
    private void adjustMoneyVisualQuietly(PlayerInfo playerInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoneyQuietly(playerInfo);
    }
    private void updatePropertyIcons(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.updatePropertyIconVisual(playerInfo, propertyInfo);
    }
    private void updateTurnPlayerHighlight() {
        int turnPlayerIndex = GameState.game.IndexOfTurnPlayer;
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            getPlayerPanel(i).toggleHighlightImage(i == turnPlayerIndex);
        }
    }
    private void updateGOOJFCardIcon(PlayerInfo playerInfo, CardType cardType) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        StartCoroutine(playerPanel.toggleGOOJFIcon(cardType));
    }
    private void checkForUpdatesAfterBackButtonPushed() {
        List<PropertyGroupIcon> iconsToUpdate = new();
        for (int i = 0; i < transform.childCount; i++) {
            PlayerInfo playerInfo = GameState.game.getPlayerInfo(i);
            if (!playerInfo.IsActive) continue;

            PlayerPanel playerPanel = getPlayerPanel(i);
            List<PropertyGroupIcon> needingUpdateOnThisPanel = playerPanel.propertyGroupIconsNeedingAnUpdate();
            foreach (PropertyGroupIcon PGI in needingUpdateOnThisPanel) iconsToUpdate.Add(PGI);
        }

        StartCoroutine(updateIconsInSequence(iconsToUpdate));
    }
    private void changeMoneyAdjustListening(bool quietly) {
        if (quietly) {
            uiPipelineEvents.sub_MoneyAdjustment(adjustMoneyVisualQuietly);
            uiPipelineEvents.unsub_MoneyAdjustment(adjustMoneyVisual);
        }
        else {
            uiPipelineEvents.sub_MoneyAdjustment(adjustMoneyVisual);
            uiPipelineEvents.unsub_MoneyAdjustment(adjustMoneyVisualQuietly);
        }
    }
    private void updateVisualsAfterTradeListening() {
        TradeInfo completedTrade = GameState.game.CompletedTrade;

        StartCoroutine(updateVisualsAfterTrade(completedTrade));
    }
    #endregion



    #region private
    private PlayerPanel getPlayerPanel(int index) {
        return transform.GetChild(index).GetComponent<PlayerPanel>();
    }
    private IEnumerator updateIconsInSequence(List<PropertyGroupIcon> iconsToUpdate) {
        foreach (PropertyGroupIcon propertyGroupIcon in iconsToUpdate) {
            yield return StartCoroutine(propertyGroupIcon.pulseAndUpdateWithPop());
        }

        yield return WaitFrames.Instance.frames(30);
        ManagePropertiesEventHub.Instance.call_UpdateBoardAfterManagePropertiesClosed();
    }
    private IEnumerator updateIconsFromTradeSimultaneously(PlayerInfo playerOne, PlayerInfo playerTwo) {
        PropertyGroupIcon[] getIcons(PlayerInfo playerInfo) {
            PlayerPanel playerPanel = getPlayerPanel(playerInfo.Index);
            return playerPanel.PropertyGroupIcons;
        }
        PropertyGroupIcon[] iconsOne = getIcons(playerOne);
        PropertyGroupIcon[] iconsTwo = getIcons(playerTwo);
        for (int i = 0; i < 10; i++) {
            PropertyGroupIcon iconOne = iconsOne[i];
            if (!iconOne.NeedsToUpdate) continue;
            PropertyGroupIcon iconTwo = iconsTwo[i];

            StartCoroutine(iconOne.pulseAndUpdateWithPop());
            StartCoroutine(iconTwo.pulseAndUpdate());
            yield return WaitFrames.Instance.frames(30);
        }
    }
    private IEnumerator updateVisualsAfterTrade(TradeInfo completedTrade) {
        IEnumerator exchangeMoney() {
            uiEventHub.call_UpdateUIMoney(
                completedTrade.MoneyGivingPlayer,
                completedTrade.MoneyReceivingPlayer
            );
            yield return WaitFrames.Instance.frames(InterfaceConstants.FRAMES_FOR_MONEY_UPDATE);
        }
        IEnumerator exchangeCard(CardType cardType) {
            PlayerPanel playerPanelOne = getPlayerPanel(completedTrade.PlayerOne.Index);
            PlayerPanel playerPanelTwo = getPlayerPanel(completedTrade.PlayerTwo.Index);

            if (playerPanelOne.needsGOOJFIconAdjusted(cardType)) {
                if (completedTrade.PlayerOne.hasGOOJFCardOfType(cardType)) {
                    yield return playerPanelTwo.toggleGOOJFIcon(cardType);
                    yield return WaitFrames.Instance.frames(10);
                    yield return playerPanelOne.toggleGOOJFIcon(cardType);
                }
                else {
                    yield return playerPanelOne.toggleGOOJFIcon(cardType);
                    yield return WaitFrames.Instance.frames(10);
                    yield return playerPanelTwo.toggleGOOJFIcon(cardType);
                }
            }
        }

        yield return WaitFrames.Instance.frames(50);
        if (completedTrade.MoneyWasExchanged) yield return exchangeMoney();
        if (completedTrade.PropertyWasExchanged) yield return updateIconsFromTradeSimultaneously(
            completedTrade.PlayerOne,
            completedTrade.PlayerTwo
        );
        if (completedTrade.CardWasExchanged) {
            yield return exchangeCard(CardType.CHANCE);
            yield return exchangeCard(CardType.COMMUNITY_CHEST);
        }
        for (int i = 0; i < 30; i++) yield return null;
        tradeEvents.call_AllVisualsUpdatedAfterTradeFinalised();
    }
    #endregion
}
