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
    private List<ManagePropertiesTokenIcon> tokenIcons;
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
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(backButtonPressed);
        AuctionEventHub.Instance.sub_AuctionRemainingBuildingsButtonClicked(raise);
        AuctionEventHub.Instance.sub_AuctionBuildingsBackButtonClicked(() => drop(selectedPlayer));
    }
    #endregion



    #region public
    public PlayerInfo SelectedPlayer {
        get { return selectedPlayer; }
        set {
            selectedPlayer = value;
            foreach (ManagePropertiesTokenIcon mpTokenIcon in tokenIcons) {
                if (mpTokenIcon.PlayerInfo == selectedPlayer) mpTokenIcon.select();
                else mpTokenIcon.deselect();
            }
            moneyAdjuster.adjustMoneyQuietly(value);
            moneyAdjuster.interruptWobbling();
        }
    }
    public BuildingType BuildingTypeAuctioned { get; set; }
    public bool IsRegularRefreshMode { get; private set; }
    public IEnumerator returnForBuildingPlacement(PlayerInfo auctionWinner) {
        foreach (ManagePropertiesTokenIcon mpTokenIcon in tokenIcons) {
            mpTokenIcon.disableRaycaster();
        }
        SelectedPlayer = auctionWinner;
        IsRegularRefreshMode = false;
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualClear();
        backButton.interactable = false;
        auctionHousesButton.interactable = false;
        auctionHotelsButton.interactable = false;
        yield return dropCoroutine();
    }
    public void resetAfterBuildingPlacement() {
        IsRegularRefreshMode = true;
        backButton.interactable = true;
        foreach (ManagePropertiesTokenIcon mpTokenIcon in tokenIcons) {
            mpTokenIcon.enableRaycaster();
        }
    }
    #endregion



    #region private
    private void drop(PlayerInfo selectectedPlayer) {
        tokenIcons = getTokenIcons();
        SelectedPlayer = selectectedPlayer;

        UIEventHub.Instance.call_FadeScreenCoverIn(1f);
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(selectectedPlayer, IsRegularRefreshMode);
        StartCoroutine(dropCoroutine());
    }
    private void raise() {
        StartCoroutine(raiseCoroutine());
    }
    private void backButtonPressed() {
        tokenIcons = null;
        UIEventHub.Instance.call_FadeScreenCoverOut();
        raise();
    }
    private void adjustMoneyVisual(PlayerInfo playerInfo) {
        moneyAdjuster.adjustMoney(playerInfo);
    }
    private List<ManagePropertiesTokenIcon> getTokenIcons() {
        ManagePropertiesTokenIcon getComponent(int i) {
            return tokenIconContainerRT.GetChild(i).GetChild(0).GetComponent<ManagePropertiesTokenIcon>();
        }


        List<ManagePropertiesTokenIcon> tokenIcons = new();
        IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
        for (int i = 0; i < GameConstants.MAX_PLAYERS; i++) {
            if (i < activePlayers.Count()) {
                PlayerInfo activePlayer = activePlayers.ElementAt(i);
                ManagePropertiesTokenIcon managePropertiesTokenIcon = getComponent(i);
                managePropertiesTokenIcon.setup(activePlayer);
                tokenIcons.Add(managePropertiesTokenIcon);
            }
            else tokenIconContainerRT.GetChild(i).gameObject.SetActive(false);
        }

        return tokenIcons;
    }
    #endregion



    #region Moving coroutines
    private IEnumerator dropCoroutine() {
        ManagePropertiesEventHub.Instance.call_PanelPaused();

        SoundPlayer.Instance.play_Swoop();
        UIPipelineEventHub.Instance.sub_MoneyAdjustment(adjustMoneyVisual);
        float length = offScreenY - onScreenY;
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_DROP; i++) {
            float yPos = LinearValue.exe(i, offScreenY, onScreenY, FrameConstants.MANAGE_PROPERTIES_DROP);
            rt.anchoredPosition = new Vector2(0f, yPos);
            yield return null;
        }
        rt.anchoredPosition = new Vector2(0f, onScreenY);

        ManagePropertiesEventHub.Instance.call_PanelUnpaused();
    }
    private IEnumerator raiseCoroutine() {
        ManagePropertiesEventHub.Instance.call_PanelPaused();

        SoundPlayer.Instance.play_Swoop();
        UIPipelineEventHub.Instance.unsub_MoneyAdjustment(adjustMoneyVisual);
        float length = offScreenY - onScreenY;
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_DROP; i++) {
            float yPos = LinearValue.exe(i, onScreenY, offScreenY, FrameConstants.MANAGE_PROPERTIES_DROP);
            rt.anchoredPosition = new Vector2(0f, yPos);
            yield return null;
        }
        rt.anchoredPosition = new Vector2(0f, offScreenY);

        ManagePropertiesEventHub.Instance.call_PanelUnpaused();
    }
    #endregion
}
