using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            ScreenOverlayStarterEventHub overlayStarterEvents = ScreenOverlayStarterEventHub.Instance;
            ScreenOverlayFunctionEventHub overlayFunctionEvents = ScreenOverlayFunctionEventHub.Instance;



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
            uiEvents.sub_UpdateIconsAfterResolveDebt(updateIconsAfterResolveDebt);
            tradeEvents.sub_UpdateVisualsAfterTradeFinalised(updateVisualsAfterTrade);

            uiPipelineEvents.sub_PlayerEliminated(eliminatePlayer);

            overlayStarterEvents.sub_PurchaseQuestion((PlayerInfo pl, PropertyInfo pr) => bringPlayerPanelOverScreenCover(pl));
            overlayStarterEvents.sub_IncomeTaxQuestion((PlayerInfo pl) => bringPlayerPanelOverScreenCover(pl));
            overlayStarterEvents.sub_ResolveMortgage((PlayerInfo pl, PropertyInfo pr) => bringPlayerPanelOverScreenCover(pl));
            overlayFunctionEvents.sub_PurchaseYesClicked(bringBackPlayerPanelAfterFade);
            overlayFunctionEvents.sub_PurchaseNoClicked(bringBackPlayerPanelImmediately);
            overlayFunctionEvents.sub_IncomeTaxAnswered(bringBackPlayerPanelAfterFade);
            overlayFunctionEvents.sub_UnmortgageClicked(bringBackPlayerPanelAfterFade);
            overlayFunctionEvents.sub_KeepMortgageClicked(bringBackPlayerPanelAfterFade);

            overlayStarterEvents.sub_WinnerAnnounced(removeHighlight);
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
    public PlayerInfo[] getPlayersNeedingMoneyUIUpdate() {
        List<PlayerInfo> playersToUpdate = new List<PlayerInfo>();
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            PlayerInfo playerInfo = GameState.game.getPlayerInfo(i);
            if (!playerInfo.IsActive) continue;

            PlayerPanel playerPanel = getPlayerPanel(i);
            if (!playerPanel.NeedsMoneyUpdate) continue;

            playersToUpdate.Add(playerInfo);
        }
        return playersToUpdate.ToArray();
    }
    #endregion



    #region Miscellaneous updates
    private void updateTurnPlayerHighlight() {
        int turnPlayerIndex = GameState.game.TurnPlayer.Index;
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            getPlayerPanel(i).toggleHighlightImage(i == turnPlayerIndex);
        }
    }
    private void removeHighlight(PlayerInfo winner) {
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            getPlayerPanel(i).toggleHighlightImage(false);
        }
    }
    private void eliminatePlayer(PlayerInfo eliminatedPlayer) {
        int index = eliminatedPlayer.Index;
        PlayerBeingEliminated playerBeingEliminated = getPlayerPanel(index).GetComponent<PlayerBeingEliminated>();
        StartCoroutine(playerBeingEliminated.eliminatePlayerSequence());
    }
    private void bringPlayerPanelOverScreenCover(PlayerInfo playerInfo) {
        PlayerPanel playerPanel = playerPanels[playerInfo.Index];
        playerPanel.toggleOverScreenCover(true);
    }
    private void bringBackPlayerPanelAfterFade() {
        WaitFrames.Instance.beforeAction(
            FrameConstants.SCREEN_COVER_TRANSITION,
            () => {
                foreach (PlayerPanel playerPanel in playerPanels) {
                    playerPanel.toggleOverScreenCover(false);
                }
            }
        );
    }
    private void bringBackPlayerPanelImmediately() {
        foreach (PlayerPanel playerPanel in playerPanels) {
            playerPanel.toggleOverScreenCover(false);
        }
    }
    #endregion
    #region Money updates
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
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        SoundPlayer.Instance.play_MoneyChing();
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoney(playerInfo);
    }
    private void adjustMoneyVisualQuietly(PlayerInfo playerInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoneyQuietly(playerInfo);
    }
    private void adjustMoneyVisuals(PlayerInfo playerOne, PlayerInfo playerTwo) {
        SoundPlayer.Instance.play_MoneyChing();
        getPlayerPanel(playerOne.Index).adjustMoney(playerOne);
        getPlayerPanel(playerTwo.Index).adjustMoney(playerTwo);
    }
    private void adjustMoneyVisuals(PlayerInfo[] players) {
        SoundPlayer.Instance.play_MoneyChing();
        foreach (PlayerInfo playerInfo in players) {
            int index = playerInfo.Index;
            PlayerPanel playerPanel = getPlayerPanel(index);
            playerPanel.adjustMoney(playerInfo);
        }
    }
    #endregion
    #region Icon updates
    private void updatePropertyIcon(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        StartCoroutine(playerPanel.PropertyGroupIconSection.updatePropertyIconVisual(playerInfo, propertyInfo));
    }
    private void updateGOOJFCardIcon(PlayerInfo playerInfo, CardType cardType) {
        int playerIndex = playerInfo.Index;
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        StartCoroutine(playerPanel.toggleGOOJFIcon(cardType));
    }
    private void updateAllExpiredPropertyIcons() {
        IEnumerator updateIconsInSequence(List<PropertyGroupIcon> iconsToUpdate) {
            foreach (PropertyGroupIcon propertyGroupIcon in iconsToUpdate) {
                SoundPlayer.Instance.play_Pop();
                yield return propertyGroupIcon.pulseAndUpdate();
            }

            yield return WaitFrames.Instance.frames(30);
            UIEventHub.Instance.call_UpdateExpiredBoardVisuals();
        }


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
    private void updateVisualsAfterTrade() {
        IEnumerator updateIconsFromTradeSimultaneously(PlayerInfo playerOne, PlayerInfo playerTwo) {
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

                SoundPlayer.Instance.play_Pop();
                StartCoroutine(iconOne.pulseAndUpdate());
                StartCoroutine(iconTwo.pulseAndUpdate());
                yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
            }
        }
        IEnumerator exchangeMoney(TradeInfo completedTrade) {
            UIEventHub.Instance.call_UpdateUIMoney(new PlayerInfo[] {
                    completedTrade.MoneyGivingPlayer,
                    completedTrade.MoneyReceivingPlayer
                });
            yield return WaitFrames.Instance.frames(FrameConstants.MONEY_UPDATE);
        }
        IEnumerator exchangeCard(TradeInfo completedTrade, CardType cardType) {
            PlayerPanel playerPanelOne = getPlayerPanel(completedTrade.PlayerOne.Index);
            PlayerPanel playerPanelTwo = getPlayerPanel(completedTrade.PlayerTwo.Index);
            if (!playerPanelOne.needsGOOJFIconAdjusted(cardType)) yield break;


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
        IEnumerator updateVisualsAfterTradeCoroutine(TradeInfo completedTrade) {
            yield return WaitFrames.Instance.frames(50);
            if (completedTrade.MoneyWasExchanged) yield return exchangeMoney(completedTrade);
            if (completedTrade.PropertyWasExchanged) yield return updateIconsFromTradeSimultaneously(
                completedTrade.PlayerOne,
                completedTrade.PlayerTwo
            );
            if (completedTrade.CardWasExchanged) {
                yield return exchangeCard(completedTrade, CardType.CHANCE);
                yield return exchangeCard(completedTrade, CardType.COMMUNITY_CHEST);
            }
            for (int i = 0; i < 30; i++) yield return null;
            TradeEventHub.Instance.call_AllVisualsUpdatedAfterTradeFinalised();
        }


        TradeInfo completedTrade = GameState.game.CompletedTrade;
        StartCoroutine(updateVisualsAfterTradeCoroutine(completedTrade));
    }
    private void updateIconsAfterResolveDebt() {
        IEnumerator updateIconGroupsSimultaneously(IEnumerable<PlayerPanel> playerPanels) {
            IEnumerable<PropertyGroupIconSection> pgiSections = playerPanels.Select(x => x.PropertyGroupIconSection);
            for (int i = 0; i < 10; i++) {
                IEnumerable<PropertyGroupIcon> pgis = pgiSections.Select(x => x.getIcon(i));
                IEnumerable<PropertyGroupIcon> needingUpdate = pgis.Where(x => x.NeedsToUpdate);
                if (needingUpdate.Count() == 0) continue;

                SoundPlayer.Instance.play_Pop();
                foreach (PropertyGroupIcon pgi in needingUpdate) {
                    StartCoroutine(pgi.pulseAndUpdate());
                }
                yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
            }
        }
        IEnumerator exchangeCard(IEnumerable<PlayerPanel> playerPanels, CardType cardType) {
            IEnumerable<PlayerPanel> needingCardUpdate = playerPanels.Where(x => x.needsGOOJFIconAdjusted(cardType));
            if (needingCardUpdate.Count() == 0) yield break;
            if (needingCardUpdate.Count() != 2) throw new System.Exception("Players needing a card update isn't two.");


            PlayerPanel hasCard = needingCardUpdate.First(x => x.PlayerInfo.hasGOOJFCardOfType(cardType));
            PlayerPanel gaveCard = needingCardUpdate.First(x => x != hasCard);
            StartCoroutine(gaveCard.toggleGOOJFIcon(cardType));
            yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
            StartCoroutine(hasCard.toggleGOOJFIcon(cardType));
            yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);   
        }
        IEnumerator updateIconsAfterResolveDebtCoroutine(IEnumerable<PlayerPanel> playerPanels) {
            yield return updateIconGroupsSimultaneously(playerPanels);
            yield return exchangeCard(playerPanels, CardType.CHANCE);
            yield return exchangeCard(playerPanels, CardType.COMMUNITY_CHEST);
            UIEventHub.Instance.call_UpdateExpiredBoardVisuals();
        }


        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        IEnumerable<PlayerPanel> playerPanels = activePlayers.Select(x => getPlayerPanel(x.Index));
        StartCoroutine(updateIconsAfterResolveDebtCoroutine(playerPanels));
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
    #endregion
}
