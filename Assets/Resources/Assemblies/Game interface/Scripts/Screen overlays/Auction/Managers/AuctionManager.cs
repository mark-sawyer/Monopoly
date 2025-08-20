using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AuctionManager<T> : ScreenOverlay<T> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform auctionPanelParentRT;
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject onePlayer;
    [SerializeField] private GameObject twoPlayers;
    [SerializeField] private GameObject bottom;
    private int currentBid;
    private PlayerInfo biddingPlayer;
    private const float IDEAL_SCALE = 1.6f;



    #region public
    public static AuctionManager<T> Instance { get; protected set; }
    public int CurrentBid {
        get { return currentBid; }
        protected set { currentBid = value; }
    }
    public PlayerInfo BiddingPlayer {
        get { return biddingPlayer; }
        protected set { biddingPlayer = value; }
    }
    public void acceptNewBid(int newBid, PlayerInfo newBiddingPlayer) {
        currentBid = newBid;
        biddingPlayer = newBiddingPlayer;
    }
    public abstract void auctionFinished();
    #endregion



    #region protected
    protected RectTransform AuctionPanelParentRT => auctionPanelParentRT;
    protected void composePanel(List<PlayerInfo> playersParticipating) {
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


        Instantiate(top, auctionPanelParentRT);
        int totalPlayers = playersParticipating.Count();
        int remainingPlayers = totalPlayers;
        while (remainingPlayers > 0) {
            if (remainingPlayers >= 2) {
                addNextPart(twoPlayers);
                PlayerInfo[] playersAdded = new PlayerInfo[2] {
                    playersParticipating[totalPlayers - remainingPlayers],
                    playersParticipating[totalPlayers - remainingPlayers + 1]
                };
                setupLastAuctionRow(playersAdded);
                remainingPlayers -= 2;
            }
            else {
                addNextPart(onePlayer);
                PlayerInfo[] playersAdded = new PlayerInfo[1] { playersParticipating[totalPlayers - 1] };
                setupLastAuctionRow(playersAdded);
                remainingPlayers -= 1;
            }
        }
        addNextPart(bottom);
    }
    protected void scalePanel() {
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
    protected void movePanelToStartingPosition() {
        float auctionHeight = getScaledAuctionHeight();
        auctionPanelParentRT.anchoredPosition = new Vector2(0f, auctionHeight + 150f);
    }
    protected IEnumerator drop() {
        SoundOnlyEventHub.Instance.call_OtherChime();
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
    #endregion
}
