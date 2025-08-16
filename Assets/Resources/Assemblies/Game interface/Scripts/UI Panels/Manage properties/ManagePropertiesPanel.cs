using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ManagePropertiesPanel : MonoBehaviour {
    #region Internal references
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform tokenIconContainerRT;
    [SerializeField] private Transform propertSectionsTransform;
    [SerializeField] private MoneyAdjuster moneyAdjuster;
    [SerializeField] private Button backButton;
    [SerializeField] private Button auctionHousesButton;
    [SerializeField] private Button auctionHotelsButton;
    #endregion
    #region Private attributes
    private PlayerInfo selectedPlayer;
    private float offScreenY;
    private float onScreenY;
    #endregion
    #region Numeric constants
    private const float VERTICAL_PROPORTION = 800f / 1080f;
    private const float HEIGHT_ABOVE_CANVAS_PROPORTION = 5f / 36f;
    #endregion



    #region Singleton boilerplate
    public static ManagePropertiesPanel Instance { get; private set; }
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
        void setScaleAndPosition() {
            float canvasHeight = ((RectTransform)transform.parent).rect.height;
            float thisHeight = rt.rect.height;
            float scale = VERTICAL_PROPORTION * canvasHeight / thisHeight;
            transform.localScale = new Vector3(scale, scale, scale);
            offScreenY = canvasHeight * HEIGHT_ABOVE_CANVAS_PROPORTION;
            onScreenY = -canvasHeight * ((1f + VERTICAL_PROPORTION) / 2f);
            rt.anchoredPosition = new Vector3(0f, offScreenY, 0f);
        }


        setScaleAndPosition();
        IsRegularRefreshMode = true;
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(() => drop(GameState.game.TurnPlayer));
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(raise);
        AuctionEventHub.Instance.sub_AuctionRemainingBuildingsButtonClicked(raiseForAuction);
        AuctionEventHub.Instance.sub_AuctionBuildingsBackButtonClicked(() => drop(selectedPlayer));
    }
    #endregion



    #region public
    public PlayerInfo SelectedPlayer {
        get { return selectedPlayer; }
        set {
            selectedPlayer = value;
            moneyAdjuster.adjustMoneyQuietly(value);
            moneyAdjuster.interruptWobbling();
        }
    }
    public BuildingType BuildingTypeAuctioned { get; set; }
    public bool IsRegularRefreshMode { get; private set; }
    public ManagePropertiesTokenIcon getManagePropertiesTokenIcon(PlayerInfo playerInfo) {
        ManagePropertiesTokenIcon returnManagePropertiesTokenIcon = null;
        for (int i = 0; i < GameState.game.ActivePlayers.Count(); i++) {
            Transform child = tokenIconContainerRT.GetChild(i);
            ManagePropertiesTokenIcon managePropertiesTokenIcon = child.GetChild(0).GetComponent<ManagePropertiesTokenIcon>();
            PlayerInfo thisPlayerInfo = managePropertiesTokenIcon.PlayerInfo;
            if (thisPlayerInfo == playerInfo) {
                returnManagePropertiesTokenIcon = managePropertiesTokenIcon;
                break;
            }
        }
        return returnManagePropertiesTokenIcon;
    }
    public IEnumerator returnForBuildingPlacement(PlayerInfo auctionWinner) {
        foreach (PlayerInfo activePlayer in GameState.game.ActivePlayers) {
            ManagePropertiesTokenIcon managePropertiesTokenIcon = getManagePropertiesTokenIcon(activePlayer);
            managePropertiesTokenIcon.setForBuildingAuctionWinner(auctionWinner);
        }
        IsRegularRefreshMode = false;
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualClear();
        backButton.interactable = false;
        auctionHousesButton.interactable = false;
        auctionHotelsButton.interactable = false;
        yield return dropCoroutine(false);
    }
    public void resetAfterBuildingPlacement() {
        IsRegularRefreshMode = true;
        backButton.interactable = true;
        foreach (PlayerInfo activePlayer in GameState.game.ActivePlayers) {
            ManagePropertiesTokenIcon managePropertiesTokenIcon = getManagePropertiesTokenIcon(activePlayer);
            managePropertiesTokenIcon.setForRegularSelection();
        }
    }
    #endregion



    #region private
    private void drop(PlayerInfo selectectedPlayer) {
        void setTokenIcons(PlayerInfo selectedPlayer) {
            ManagePropertiesTokenIcon getComponent(int i) {
                return tokenIconContainerRT.GetChild(i).GetChild(0).GetComponent<ManagePropertiesTokenIcon>();
            }

            IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
            for (int i = 0; i < GameConstants.MAX_PLAYERS; i++) {
                if (i < activePlayers.Count()) {
                    PlayerInfo activePlayer = activePlayers.ElementAt(i);
                    ManagePropertiesTokenIcon managePropertiesTokenIcon = getComponent(i);
                    managePropertiesTokenIcon.setup(activePlayer);
                    if (activePlayer == selectedPlayer) {
                        managePropertiesTokenIcon.selectQuietly();
                    }
                    else {
                        managePropertiesTokenIcon.deselect();
                    }
                }
                else tokenIconContainerRT.GetChild(i).gameObject.SetActive(false);
            }
        }


        setTokenIcons(selectectedPlayer);
        UIEventHub.Instance.call_FadeScreenCoverIn(1f);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectectedPlayer, IsRegularRefreshMode);
        UIPipelineEventHub.Instance.sub_MoneyAdjustment(adjustMoneyVisual);
        StartCoroutine(dropCoroutine(true));
    }
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        moneyAdjuster.adjustMoney(playerInfo);
    }
    private void raise() {
        UIPipelineEventHub.Instance.unsub_MoneyAdjustment(adjustMoneyVisual);
        UIEventHub.Instance.call_FadeScreenCoverOut();
        StartCoroutine(raiseCoroutine());
    }
    private void raiseForAuction() {
        StartCoroutine(raiseCoroutine());
    }
    #endregion



    #region Moving coroutines
    private IEnumerator raiseCoroutine() {
        SoundOnlyEventHub.Instance.call_Swoop();
        float length = offScreenY - onScreenY;
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_DROP; i++) {
            float yPos = LinearValue.exe(i, onScreenY, offScreenY, FrameConstants.MANAGE_PROPERTIES_DROP);
            rt.anchoredPosition = new Vector2(0f, yPos);
            yield return null;
        }
        rt.anchoredPosition = new Vector2(0f, offScreenY);
    }
    private IEnumerator dropCoroutine(bool backButtonOnAtEnd) {
        SoundOnlyEventHub.Instance.call_Swoop();
        float length = offScreenY - onScreenY;
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_DROP; i++) {
            float yPos = LinearValue.exe(i, offScreenY, onScreenY, FrameConstants.MANAGE_PROPERTIES_DROP);
            rt.anchoredPosition = new Vector2(0f, yPos);
            yield return null;
        }
        rt.anchoredPosition = new Vector2(0f, onScreenY);
        backButton.interactable = backButtonOnAtEnd;
    }
    #endregion
}
