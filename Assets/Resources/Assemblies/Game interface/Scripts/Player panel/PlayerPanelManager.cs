using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelManager : MonoBehaviour {
    [SerializeField] private GameObject playerPanelPrefab;
    [SerializeField] private GameEvent<PlayerInfo> moneyAdjustment;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPropertyAdjustment;
    private const float GAP = 3;



    #region MonoBehaviour
    private void Start() {
        instantiatePanels();
        scalePanels();
        associateWithPlayers();
        subscribeToEvents();
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
    private void associateWithPlayers() {
        IEnumerable<PlayerInfo> players = GameState.game.PlayerInfos;
        int i = 0;
        foreach (PlayerInfo player in players) {
            transform.GetChild(i).GetComponent<PlayerPanel>().setup(player);
            i += 1;
        }
    }
    private void subscribeToEvents() {
        moneyAdjustment.Listeners += adjustMoneyVisual;
        playerPropertyAdjustment.Listeners += updatePropertyIcons;
    }
    #endregion



    #region Events
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        int playerIndex = GameState.game.getPlayerIndex(playerInfo);
        PlayerPanel playerPanel = transform.GetChild(playerIndex).GetComponent<PlayerPanel>();
        playerPanel.adjustMoney(playerInfo);
    }
    private void updatePropertyIcons(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        int playerIndex = GameState.game.getPlayerIndex(playerInfo);
        PlayerPanel playerPanel = transform.GetChild(playerIndex).GetComponent<PlayerPanel>();
        playerPanel.updatePropertyIconVisual(playerInfo, propertyInfo);
    }
    #endregion
}
