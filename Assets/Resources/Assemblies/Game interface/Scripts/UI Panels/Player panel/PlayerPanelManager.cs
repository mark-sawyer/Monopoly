using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerPanelManager : MonoBehaviour {
    #region External references
    [SerializeField] private GameObject playerPanelPrefab;
    [SerializeField] private GameEvent nextTurnPlayerUI;
    [SerializeField] private PlayerEvent moneyAdjustmentUI;
    [SerializeField] private PlayerPropertyEvent playerPropertyAdjustmentUI;
    [SerializeField] private PlayerCardTypeEvent playerGetsGOOJFCardUI;
    [SerializeField] private CardTypeEvent playedUsedGOOJFCardUI;
    [SerializeField] private GameEvent updateIconsAfterManagePropertiesClosed;
    [SerializeField] private GameEvent iconsUpdatedAfterManagePropertiesClosed;
    #endregion
    private const float GAP = 3;



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
        moneyAdjustmentUI.Listeners += adjustMoneyVisual;
        playerPropertyAdjustmentUI.Listeners += updatePropertyIcons;
        nextTurnPlayerUI.Listeners += updateTurnPlayerHighlight;
        playerGetsGOOJFCardUI.Listeners += updateGOOJFCardIcons;
        playedUsedGOOJFCardUI.Listeners += (CardType cardType) => updateGOOJFCardIcons(GameState.game.TurnPlayer, cardType);
        updateIconsAfterManagePropertiesClosed.Listeners += checkForUpdatesAfterBackButtonPushed;
    }
    #endregion



    #region Listeners
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        int playerIndex = GameState.game.getPlayerIndex(playerInfo);
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.adjustMoney(playerInfo);
    }
    private void updatePropertyIcons(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        int playerIndex = GameState.game.getPlayerIndex(playerInfo);
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        playerPanel.updatePropertyIconVisual(playerInfo, propertyInfo);
    }
    private void updateTurnPlayerHighlight() {
        int turnPlayerIndex = GameState.game.IndexOfTurnPlayer;
        for (int i = 0; i < GameState.game.NumberOfPlayers; i++) {
            getPlayerPanel(i).toggleHighlightImage(i == turnPlayerIndex);
        }
    }
    private void updateGOOJFCardIcons(PlayerInfo playerInfo, CardType cardType) {
        int playerIndex = GameState.game.getPlayerIndex(playerInfo);
        PlayerPanel playerPanel = getPlayerPanel(playerIndex);
        bool hasCard = playerInfo.hasGOOJFCardOfType(cardType);
        playerPanel.toggleGOOJFIcon(cardType, hasCard);
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

        StartCoroutine(updateIconsPostManagePropertiesClosed(iconsToUpdate));
    }
    #endregion



    #region private
    private PlayerPanel getPlayerPanel(int index) {
        return transform.GetChild(index).GetComponent<PlayerPanel>();
    }
    private IEnumerator updateIconsPostManagePropertiesClosed(List<PropertyGroupIcon> iconsToUpdate) {
        foreach (PropertyGroupIcon propertyGroupIcon in iconsToUpdate) {
            StartCoroutine(propertyGroupIcon.pulseAndUpdate());
            propertyGroupIcon.setNewState();
            for (int i = 0; i < 22; i++) yield return null;
        }
        iconsUpdatedAfterManagePropertiesClosed.invoke();
    }
    #endregion
}
