using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AuctionManager : MonoBehaviour {
    #region Internal references
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform droppingPanelRT;
    [SerializeField] private Transform visualHolderTransform;
    #endregion
    #region External references
    [SerializeField] private GameObject twoPlayers;
    [SerializeField] private GameObject twoPlayersFinal;
    [SerializeField] private GameObject onePlayerFinal;
    #endregion
    private int minimumBid;
    private const float DEFAULT_Y_POSITION = 2000f;
    private const float IDEAL_SCALE = 1.6f;




    #region Singleton boilerplate
    public static AuctionManager Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region public
    public int MinimumBid => minimumBid;
    public void appear(List<PlayerInfo> playerInfos) {
        minimumBid = 1;
        composeParts(playerInfos);
        adjustPosition();
        adjustScale();
        StartCoroutine(drop());
    }
    public void takeInVisualChild(Transform visual) {
        visual.SetParent(visualHolderTransform);
    }
    #endregion



    #region private
    private void composeParts(List<PlayerInfo> playerInfos) {
        void addNewRow(GameObject rowPrefab, PlayerInfo[] addedPlayers) {
            float currentHeight = totalHeight();
            GameObject newRow = Instantiate(rowPrefab, droppingPanelRT);
            RectTransform newRT = (RectTransform)newRow.transform;
            newRT.anchoredPosition = new Vector2(0f, -currentHeight);


            int childCount = newRT.childCount;
            int playersBeingAdded = addedPlayers.Length;
            for (int i = 0; i < playersBeingAdded; i++) {
                PlayerInfo playerInfo = addedPlayers[i];
                Transform playerSectionTransform = newRT.GetChild(childCount - playersBeingAdded + i);
                AuctionPlayerSection auctionPlayerSection = playerSectionTransform.GetComponent<AuctionPlayerSection>();
                auctionPlayerSection.setup(playerInfo);
            }
        }

        int playerCount = playerInfos.Count;
        int remainingSections = playerCount;        
        while (remainingSections > 0) {
            if (remainingSections > 2) {
                PlayerInfo[] addedPlayers = new PlayerInfo[2] {
                    playerInfos[playerCount - remainingSections],
                    playerInfos[playerCount - remainingSections + 1],
                };
                addNewRow(twoPlayers, addedPlayers);
                remainingSections -= 2;
            }
            else if (remainingSections == 2) {
                PlayerInfo[] addedPlayers = new PlayerInfo[2] {
                    playerInfos[playerCount - 2],
                    playerInfos[playerCount - 1],
                };
                addNewRow(twoPlayersFinal, addedPlayers);
                remainingSections -= 2;
            }
            else {
                PlayerInfo[] addedPlayers = new PlayerInfo[1] {
                    playerInfos[playerCount - 1]
                };
                addNewRow(onePlayerFinal, addedPlayers);
                remainingSections -= 1;
            }
        }
    }
    private void adjustPosition() {
        RectTransform canvasRT = (RectTransform)rt.parent;
        float canvasHeight = canvasRT.rect.height;
        float height = totalHeight();
        float width = droppingPanelRT.rect.width;
        float startY = (canvasHeight + height) / 2f + 150f;
        droppingPanelRT.sizeDelta = new Vector2(width, height);
        droppingPanelRT.anchoredPosition = new Vector2(0f, startY);
    }
    private void adjustScale() {
        float panelWidth = droppingPanelRT.rect.width;
        float canvasWidth = ((RectTransform)transform.parent).rect.width;
        float auctionVisualX = ((RectTransform)visualHolderTransform.GetChild(0)).anchoredPosition.x;
        float scaleToTouch = 2 * auctionVisualX / (panelWidth + 35);
        float usedScale = IDEAL_SCALE < scaleToTouch ? IDEAL_SCALE : scaleToTouch;
        droppingPanelRT.localScale = new Vector3(usedScale, usedScale, usedScale);
    }
    private IEnumerator drop() {
        int frames = InterfaceConstants.FRAMES_FOR_AUCTION_DROP;
        float yStart = droppingPanelRT.anchoredPosition.y;
        Func<float, float> getY = LinearValue.getFunc(yStart, 0, frames);
        for (int i = 1; i <= frames; i++) {
            float yPos = getY(i);
            droppingPanelRT.anchoredPosition = new Vector2(0f, yPos);
            yield return null;
        }
        droppingPanelRT.anchoredPosition = Vector2.zero;
    }
    private float totalHeight() {
        float height = 0;
        for (int i = 0; i < droppingPanelRT.childCount; i++) {
            RectTransform child = (RectTransform)droppingPanelRT.GetChild(i);
            height += child.rect.height;
        }
        return height;
    }
    #endregion
}
