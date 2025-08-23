using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AuctionPropertiesManager : AuctionManager<Queue<PropertyInfo>> {
    private Queue<PropertyInfo> propertiesQueue;
    private PropertyInfo currentlyBeingAuctioned;



    #region MonoBehaviour
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
        AuctionEventHub.Instance.unsub_AuctionFinished(auctionFinished);
    }
    #endregion



    #region ScreenOverlay
    public override void setup(Queue<PropertyInfo> propertiesQueue) {
        this.propertiesQueue = propertiesQueue;
        currentlyBeingAuctioned = propertiesQueue.Dequeue();
        AuctionEventHub.Instance.sub_AuctionFinished(auctionFinished);
    }
    public override void appear() {
        composePanel(GameState.game.ActivePlayers.ToList());
        scalePanel();
        if (!AccompanyingVisualSpawner.Instance.VisualExists) {
            AccompanyingVisualSpawner.Instance.spawnAndMove(AuctionPanelParentRT, currentlyBeingAuctioned);
        }
        movePanelToStartingPosition();
        StartCoroutine(drop());
    }
    #endregion



    #region AuctionManager
    public override void auctionFinished() {
        IEnumerator completeAuctionSequence() {
            IEnumerator obtainProperty() {
                DataUIPipelineEventHub.Instance.call_MoneyAdjustment(BiddingPlayer, -CurrentBid);
                yield return WaitFrames.Instance.frames(FrameConstants.MONEY_UPDATE);
                DataUIPipelineEventHub.Instance.call_PlayerObtainedProperty(BiddingPlayer, currentlyBeingAuctioned);
                yield return WaitFrames.Instance.frames(FrameConstants.PLAYER_PANEL_ICON_POP);
            }


            SoundPlayer.Instance.play_Flourish();
            for (int i = 0; i < AuctionPanelParentRT.childCount; i++) {
                Destroy(AuctionPanelParentRT.GetChild(i).gameObject);
            }
            AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
            UIEventHub.Instance.call_FadeScreenCoverOut();
            yield return WaitFrames.Instance.frames(FrameConstants.SCREEN_COVER_TRANSITION);
            yield return WaitFrames.Instance.frames(30);
            yield return obtainProperty();
            if (propertiesQueue.Count > 0) {
                currentlyBeingAuctioned = propertiesQueue.Dequeue();
                BiddingPlayer = null;
                CurrentBid = 0;
                yield return WaitFrames.Instance.frames(50);
                UIEventHub.Instance.call_FadeScreenCoverIn(1);
                appear();
            }
            else {
                AuctionEventHub.Instance.call_AllAuctionsFinished();
            }
        }

        StartCoroutine(completeAuctionSequence());
    }
    #endregion
}
