using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionBuildingsManager : AuctionManager<BuildingType> {
    [SerializeField] private Button backButton;
    private BuildingType buildingType;



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
    public override void setup(BuildingType buildingType) {
        this.buildingType = buildingType;
        AuctionEventHub.Instance.sub_AuctionFinished(auctionFinished);
    }
    public override void appear() {
        List<PlayerInfo> getParticipatingPlayers() {
            List<PlayerInfo> participatingPlayers = new List<PlayerInfo>();
            foreach (PlayerInfo playerInfo in GameState.game.ActivePlayers) {
                int remaining = buildingType == BuildingType.HOUSE
                    ? GameState.game.BankInfo.HousesRemaining
                    : GameState.game.BankInfo.HotelsRemaining;
                int canAdd = playerInfo.buildingsCanAdd(buildingType);
                if (canAdd >= remaining) {
                    participatingPlayers.Add(playerInfo);
                }
            }
            return participatingPlayers;
        }


        List<PlayerInfo> participatingPlayers = getParticipatingPlayers();
        composePanel(participatingPlayers);
        scalePanel();
        AccompanyingVisualSpawner.Instance.spawnAndMove(AuctionPanelParentRT, buildingType);
        movePanelToStartingPosition();
        StartCoroutine(drop());
        StartCoroutine(dropBackButton());
    }
    #endregion



    #region AuctionManager
    public override void auctionFinished() {
        IEnumerator completeAuctionSequence() {
            yield return ManagePropertiesPanel.Instance.returnForBuildingPlacement(BiddingPlayer);
            yield return WaitFrames.Instance.frames(60);
            DataUIPipelineEventHub.Instance.call_MoneyAdjustment(BiddingPlayer, -CurrentBid);
            SoundPlayer.Instance.play_MoneyChing();
            yield return WaitFrames.Instance.frames(FrameConstants.MONEY_UPDATE);
            ManagePropertiesEventHub.Instance.call_WipeToCommence(BiddingPlayer);
            yield return WaitFrames.Instance.frames(FrameConstants.MANAGE_PROPERTIES_WIPE_UP * 2 + 2);
            AuctionEventHub.Instance.call_AllAuctionsFinished();
            ScreenOverlayFunctionEventHub.Instance.call_RemoveScreenOverlayKeepCover();
        }


        SoundPlayer.Instance.play_Flourish();
        for (int i = 0; i < AuctionPanelParentRT.childCount; i++) {
            Destroy(AuctionPanelParentRT.GetChild(i).gameObject);
        }
        Destroy(backButton.gameObject);
        AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
        StartCoroutine(completeAuctionSequence());
    }
    #endregion



    #region private
    private IEnumerator dropBackButton() {
        RectTransform backButtonRT = (RectTransform)backButton.transform;
        int frames = FrameConstants.SCREEN_COVER_TRANSITION;
        float x = backButtonRT.anchoredPosition.x;
        float yStart = backButtonRT.anchoredPosition.y;
        float yEnd = -20;
        Func<float, float> getY = LinearValue.getFunc(yStart, yEnd, frames);
        for (int i = 1; i <= frames; i++) {
            float y = getY(i);
            backButtonRT.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
        backButtonRT.anchoredPosition = new Vector2(x, yEnd);
        backButton.interactable = true;
    }
    #endregion
}
