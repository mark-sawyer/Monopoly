using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelManager : MonoBehaviour {
    [SerializeField] private GameObject playerPanelPrefab;
    private PlayerPanel[] playerPanels;
    private const float GAP = 3;



    #region Singleton boilerplate
    public static PlayerPanelManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        void instantiatePanels() {
            for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
                GameObject newPanel = Instantiate(playerPanelPrefab, transform);
                RectTransform rt = (RectTransform)newPanel.transform;
                float yPos = -GAP - (rt.rect.height + GAP) * i;
                rt.anchoredPosition = new Vector3(0f, yPos);
            }
        }
        void scalePanels() {
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
        void setupPanels() {
            IEnumerable<PlayerInfo> players = GameState.game.PlayerInfos;
            int i = 0;
            foreach (PlayerInfo player in players) {
                transform.GetChild(i).GetComponent<PlayerPanel>().setup(player);
                i += 1;
            }
        }
        void subscribeToEvents() {
            UIPipelineEventHub uiPipelineEvents = UIPipelineEventHub.Instance;
            UIEventHub uiEvents = UIEventHub.Instance;
            ManagePropertiesEventHub managePropertiesEvents = ManagePropertiesEventHub.Instance;
            TradeEventHub tradeEvents = TradeEventHub.Instance;



            uiEvents.sub_PrerollStateStarting(updateTurnPlayerHighlight);

            managePropertiesEvents.sub_ManagePropertiesOpened(() => changeMoneyAdjustListening(true));
            managePropertiesEvents.sub_BackButtonPressed(() => changeMoneyAdjustListening(false));

            uiPipelineEvents.sub_MoneyAdjustment(adjustMoneyVisual);
            uiPipelineEvents.sub_MoneyBetweenPlayers(adjustMoneyVisuals);
            uiEvents.sub_UpdateUIMoney(adjustMoneyVisuals);

            uiPipelineEvents.sub_PlayerPropertyAdjustment(updatePropertyIcon);

            uiPipelineEvents.sub_PlayerGetsGOOJFCard(updateGOOJFCardIcon);
            uiPipelineEvents.sub_UseGOOJFCardButtonClicked((CardType ct) => updateGOOJFCardIcon(GameState.game.TurnPlayer, ct));

            uiEvents.sub_UpdateExpiredPropertyVisuals(updateAllExpiredPropertyIcons);
            tradeEvents.sub_UpdateVisualsAfterTradeFinalised(updateVisualsAfterTradeListening);

            uiPipelineEvents.sub_PlayerEliminated(eliminatePlayer);

            ScreenOverlayEventHub.Instance.sub_WinnerAnnounced(removeHighlight);
        }


        instantiatePanels();
        scalePanels();
        setupPanels();
        subscribeToEvents();
        playerPanels = getPlayerPanels();
        playerPanels[0].toggleHighlightImage(true);
    }
    #endregion



    #region public
    public PlayerPanel getPlayerPanel(int index) {
        return playerPanels[index];
    }
    #endregion



    #region Listeners
    private void updateTurnPlayerHighlight() {
        int turnPlayerIndex = GameState.game.TurnPlayer.Index;
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            getPlayerPanel(i).toggleHighlightImage(i == turnPlayerIndex);
        }
    }
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoney(playerInfo);
    }
    private void adjustMoneyVisuals(PlayerInfo playerOne, PlayerInfo playerTwo) {
        adjustMoneyVisual(playerOne);
        adjustMoneyVisual(playerTwo);
    }
    private void adjustMoneyVisuals(PlayerInfo[] players) {
        foreach (PlayerInfo playerInfo in players) {
            adjustMoneyVisual(playerInfo);
        }
    }
    private void adjustMoneyVisualQuietly(PlayerInfo playerInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoneyQuietly(playerInfo);
    }
    private void changeMoneyAdjustListening(bool quietly) {
        if (quietly) {
            UIPipelineEventHub.Instance.sub_MoneyAdjustment(adjustMoneyVisualQuietly);
            UIPipelineEventHub.Instance.unsub_MoneyAdjustment(adjustMoneyVisual);
        }
        else {
            UIPipelineEventHub.Instance.sub_MoneyAdjustment(adjustMoneyVisual);
            UIPipelineEventHub.Instance.unsub_MoneyAdjustment(adjustMoneyVisualQuietly);
        }
    }
    private void updatePropertyIcon(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        StartCoroutine(playerPanel.PropertyGroupIconSection.updatePropertyIconVisual(playerInfo, propertyInfo));
    }
    private void updateAllExpiredPropertyIcons() {
        List<PropertyGroupIcon> iconsToUpdate = new();
        for (int i = 0; i < transform.childCount; i++) {
            PlayerInfo playerInfo = GameState.game.getPlayerInfo(i);
            if (!playerInfo.IsActive) continue;

            PlayerPanel playerPanel = playerPanels[i];
            List<PropertyGroupIcon> needingUpdateOnThisPanel = playerPanel.PropertyGroupIconSection.propertyGroupIconsNeedingAnUpdate();
            foreach (PropertyGroupIcon PGI in needingUpdateOnThisPanel) iconsToUpdate.Add(PGI);
        }

        StartCoroutine(updateIconsInSequence(iconsToUpdate));
    }
    private void updateGOOJFCardIcon(PlayerInfo playerInfo, CardType cardType) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        StartCoroutine(playerPanel.toggleGOOJFIcon(cardType));
    }
    private void updateVisualsAfterTradeListening() {
        TradeInfo completedTrade = GameState.game.CompletedTrade;

        StartCoroutine(updateVisualsAfterTrade(completedTrade));
    }
    private void eliminatePlayer(PlayerInfo eliminatedPlayer) {
        int index = eliminatedPlayer.Index;
        PlayerBeingEliminated playerBeingEliminated = getPlayerPanel(index).GetComponent<PlayerBeingEliminated>();
        StartCoroutine(playerBeingEliminated.eliminatePlayerSequence());
    }
    private void removeHighlight(PlayerInfo winner) {
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            getPlayerPanel(i).toggleHighlightImage(false);
        }
    }
    #endregion



    #region private
    private PlayerPanel[] getPlayerPanels() {
        int players = GameState.game.NumberOfPlayers;
        PlayerPanel[] playerPanels = new PlayerPanel[players];
        for (int i = 0; i < players; i++) {
            playerPanels[i] = transform.GetChild(i).GetComponent<PlayerPanel>();
        }
        return playerPanels;
    }
    private IEnumerator updateIconsInSequence(List<PropertyGroupIcon> iconsToUpdate) {
        foreach (PropertyGroupIcon propertyGroupIcon in iconsToUpdate) {
            SoundOnlyEventHub.Instance.call_AppearingPop();
            yield return propertyGroupIcon.pulseAndUpdate();
        }

        yield return WaitFrames.Instance.frames(30);
        UIEventHub.Instance.call_UpdateExpiredBoardVisuals();
    }
    private IEnumerator updateIconsFromTradeSimultaneously(PlayerInfo playerOne, PlayerInfo playerTwo) {
        PropertyGroupIconSection getIcons(PlayerInfo playerInfo) {
            PlayerPanel playerPanel = getPlayerPanel(playerInfo.Index);
            return playerPanel.PropertyGroupIconSection;
        }
        PropertyGroupIconSection groupIconsOne = getIcons(playerOne);
        PropertyGroupIconSection groupIconsTwo = getIcons(playerTwo);
        for (int i = 0; i < 10; i++) {
            PropertyGroupIcon iconOne = groupIconsOne.getIcon(i);
            if (!iconOne.NeedsToUpdate) continue;
            PropertyGroupIcon iconTwo = groupIconsTwo.getIcon(i);

            SoundOnlyEventHub.Instance.call_AppearingPop();
            StartCoroutine(iconOne.pulseAndUpdate());
            StartCoroutine(iconTwo.pulseAndUpdate());
            yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
        }
    }
    private IEnumerator updateVisualsAfterTrade(TradeInfo completedTrade) {
        IEnumerator exchangeMoney() {
            UIEventHub.Instance.call_UpdateUIMoney(new PlayerInfo[] {
                completedTrade.MoneyGivingPlayer,
                completedTrade.MoneyReceivingPlayer
            });
            yield return WaitFrames.Instance.frames(FrameConstants.MONEY_UPDATE);
        }
        IEnumerator exchangeCard(CardType cardType) {
            PlayerPanel playerPanelOne = getPlayerPanel(completedTrade.PlayerOne.Index);
            PlayerPanel playerPanelTwo = getPlayerPanel(completedTrade.PlayerTwo.Index);

            if (playerPanelOne.needsGOOJFIconAdjusted(cardType)) {
                if (completedTrade.PlayerOne.hasGOOJFCardOfType(cardType)) {
                    StartCoroutine(playerPanelTwo.toggleGOOJFIcon(cardType));
                    yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);

                    StartCoroutine(playerPanelOne.toggleGOOJFIcon(cardType));
                    yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
                }
                else {
                    StartCoroutine(playerPanelOne.toggleGOOJFIcon(cardType));
                    yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);

                    StartCoroutine(playerPanelTwo.toggleGOOJFIcon(cardType));
                    yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
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
        TradeEventHub.Instance.call_AllVisualsUpdatedAfterTradeFinalised();
    }
    #endregion
}
