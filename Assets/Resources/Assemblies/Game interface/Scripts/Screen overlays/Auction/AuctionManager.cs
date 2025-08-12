using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AuctionManager : ScreenOverlay<Queue<PropertyInfo>> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform auctionPanelParentRT;
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject onePlayer;
    [SerializeField] private GameObject twoPlayers;
    [SerializeField] private GameObject bottom;
    [SerializeField] private Button confirmButton;
    private Queue<PropertyInfo> propertiesQueue;
    private PropertyInfo currentlyBeingAuctioned;
    private int currentBid;
    private PlayerInfo biddingPlayer;
    private const float IDEAL_SCALE = 1.6f;



    #region MonoBehaviour
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        AuctionEventHub.Instance.sub_AuctionFinished(auctionFinished);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
        AuctionEventHub.Instance.unsub_AuctionFinished(auctionFinished);
    }
    #endregion



    #region ScreenOverlay
    public override void appear() {
        composePanel();
        scalePanel();
        if (!AccompanyingVisualSpawner.Instance.VisualExists) {
            AccompanyingVisualSpawner.Instance.spawnAndMove(auctionPanelParentRT, currentlyBeingAuctioned);
        }
        movePanelToStartingPosition();
        StartCoroutine(drop());
    }
    public override void setup(Queue<PropertyInfo> propertiesQueue) {
        this.propertiesQueue = propertiesQueue;
        currentlyBeingAuctioned = propertiesQueue.Dequeue();
    }
    #endregion



    #region Appearing
    private void composePanel() {
        void addNextPart(GameObject nextPart) {
            float height = getAuctionHeight();
            GameObject nextPartInstance = Instantiate(nextPart, auctionPanelParentRT);
            RectTransform nextPartRT = (RectTransform)nextPartInstance.transform;
            nextPartRT.anchoredPosition = new Vector2(0f, -height);
        }
        void setupLastAuctionRow(PlayerInfo[] playerInfos) {
            int children = auctionPanelParentRT.childCount;
            Transform lastChildTransform = auctionPanelParentRT.GetChild(children - 1);
            AuctionRowSetup auctionRowSetup = lastChildTransform.GetComponent<AuctionRowSetup>();
            auctionRowSetup.setup(playerInfos);
        }


        List<PlayerInfo> activePlayers = GameState.game.ActivePlayers.ToList();
        Instantiate(top, auctionPanelParentRT);
        int totalPlayers = activePlayers.Count();
        int remainingPlayers = totalPlayers;
        while (remainingPlayers > 0) {
            if (remainingPlayers >= 2) {
                addNextPart(twoPlayers);
                PlayerInfo[] playersAdded = new PlayerInfo[2] {
                    activePlayers[totalPlayers - remainingPlayers],
                    activePlayers[totalPlayers - remainingPlayers + 1]
                };
                setupLastAuctionRow(playersAdded);
                remainingPlayers -= 2;
            }
            else {
                addNextPart(onePlayer);
                PlayerInfo[] playersAdded = new PlayerInfo[1] { activePlayers[totalPlayers - 1] };
                setupLastAuctionRow(playersAdded);
                remainingPlayers -= 1;
            }
        }
        addNextPart(bottom);
    }
    private void scalePanel() {
        float getUsedScale() {
            if (!AccompanyingVisualSpawner.Instance.VisualExists) return IDEAL_SCALE;

            float panelWidth = auctionPanelParentRT.rect.width;
            float canvasWidth = rt.rect.width;
            float auctionVisualX = AccompanyingVisualSpawner.Instance.xPosOfVisual(rt);
            float scaleToTouch = 2 * auctionVisualX / (panelWidth + 35);
            return IDEAL_SCALE < scaleToTouch ? IDEAL_SCALE : scaleToTouch;
        }


        float usedScale = getUsedScale();
        auctionPanelParentRT.localScale = new Vector3(usedScale, usedScale, usedScale);
    }
    private void movePanelToStartingPosition() {
        float auctionHeight = getScaledAuctionHeight();
        auctionPanelParentRT.anchoredPosition = new Vector2(0f, auctionHeight + 150f);
    }
    private IEnumerator drop() {
        float auctionHeight = getScaledAuctionHeight();
        float canvasHeight = rt.rect.height;
        float gap = (canvasHeight - auctionHeight) / 2f;
        float goalY = -gap;
        float startY = auctionPanelParentRT.anchoredPosition.y;
        Func<float, float> getY = LinearValue.getFunc(startY, goalY, FrameConstants.SCREEN_COVER_TRANSITION);
        for (int i = 1; i <= FrameConstants.SCREEN_COVER_TRANSITION; i++) {
            float y = getY(i);
            auctionPanelParentRT.anchoredPosition = new Vector2(0f, y);
            yield return null;
        }
        auctionPanelParentRT.anchoredPosition = new Vector2(0f, goalY);
    }
    #endregion



    #region public
    public static AuctionManager Instance { get; private set; }
    public int CurrentBid => currentBid;
    public PlayerInfo BiddingPlayer => biddingPlayer;
    public void acceptNewBid(int newBid, PlayerInfo newBiddingPlayer) {
        currentBid = newBid;
        biddingPlayer = newBiddingPlayer;
    }
    public void auctionFinished() {
        StartCoroutine(completeAuctionSequence());
    }
    #endregion



    #region private
    private float getAuctionHeight() {
        float height = 0;
        for (int i = 0; i < auctionPanelParentRT.childCount; i++) {
            RectTransform childRT = (RectTransform)auctionPanelParentRT.GetChild(i);
            height += childRT.rect.height;
        }
        return height;
    }
    private float getScaledAuctionHeight() {
        return getAuctionHeight() * auctionPanelParentRT.localScale.x;
    }
    private IEnumerator completeAuctionSequence() {
        IEnumerator obtainTradable() {
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(biddingPlayer, -currentBid);
            yield return WaitFrames.Instance.frames(FrameConstants.MONEY_UPDATE);
            if (currentlyBeingAuctioned is PropertyInfo propertyInfo) {
                DataUIPipelineEventHub.Instance.call_PlayerObtainedProperty(biddingPlayer, propertyInfo);
                yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
            }
            else {
                DataUIPipelineEventHub.Instance.call_PlayerGetsGOOJFCard(biddingPlayer, (CardInfo)currentlyBeingAuctioned);
                yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
            }
        }


        for (int i = 0; i < auctionPanelParentRT.childCount; i++) {
            Destroy(auctionPanelParentRT.GetChild(i).gameObject);
        }
        AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
        UIEventHub.Instance.call_FadeScreenCoverOut();
        yield return WaitFrames.Instance.frames(FrameConstants.SCREEN_COVER_TRANSITION);
        yield return WaitFrames.Instance.frames(30);
        yield return obtainTradable();
        if (propertiesQueue.Count > 0) {
            currentlyBeingAuctioned = propertiesQueue.Dequeue();
            biddingPlayer = null;
            currentBid = 0;
            yield return WaitFrames.Instance.frames(50);
            UIEventHub.Instance.call_FadeScreenCoverIn(1);
            appear();
        }
        else {
            AuctionEventHub.Instance.call_AllAuctionsFinished();
        }
    }
    #endregion
}
