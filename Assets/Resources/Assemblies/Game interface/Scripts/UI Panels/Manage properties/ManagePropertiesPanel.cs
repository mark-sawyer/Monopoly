using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ManagePropertiesPanel : MonoBehaviour {
    private class EstateSectionGetter {
        private Transform propertySectionTransform;

        public EstateSectionGetter(Transform propertySectionTransform) {
            this.propertySectionTransform = propertySectionTransform;
        }
        public EstateSection exe(EstateInfo estateInfo) {
            int columnIndex = getColumnIndex(estateInfo.EstateColour);
            Transform columnTransform = propertySectionTransform.GetChild(columnIndex);
            int colourIndex = (int)estateInfo.EstateColour % 2;
            Transform colourTransform = columnTransform.GetChild(colourIndex);
            int estateOrder = estateInfo.EstateOrder;
            Transform estateSectionTransform = colourTransform.GetChild(0).GetChild(estateOrder + 1);
            EstateSection estateSection = estateSectionTransform.GetComponent<EstateSection>();
            return estateSection;
        }
        private int getColumnIndex(EstateColour estateColour) {
            switch (estateColour) {
                case EstateColour.BROWN: return 0;
                case EstateColour.LIGHT_BLUE: return 0;
                case EstateColour.PINK: return 1;
                case EstateColour.ORANGE: return 1;
                case EstateColour.RED: return 2;
                case EstateColour.YELLOW: return 2;
                case EstateColour.GREEN: return 3;
                case EstateColour.DARK_BLUE: return 3;
            }
            throw new System.Exception();
        }
    }
    #region Internal references
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform tokenIconContainerRT;
    [SerializeField] private Transform propertSectionsTransform;
    [SerializeField] private Button backButton;
    #endregion
    #region External references
    [SerializeField] private ScreenCover screenCover;
    #endregion
    #region Private attributes
    private PlayerInfo selectedPlayer;
    private float offScreenY;
    private float onScreenY;
    private EstateSectionGetter estateSectionGetter;
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
        float canvasHeight = ((RectTransform)transform.parent).rect.height;
        float thisHeight = rt.rect.height;
        float scale = VERTICAL_PROPORTION * canvasHeight / thisHeight;
        transform.localScale = new Vector3(scale, scale, scale);
        offScreenY = canvasHeight * HEIGHT_ABOVE_CANVAS_PROPORTION;
        onScreenY = -canvasHeight * ((1f + VERTICAL_PROPORTION) / 2f);
        rt.anchoredPosition = new Vector3(0f, offScreenY, 0f);
        estateSectionGetter = new EstateSectionGetter(propertSectionsTransform);
        ManagePropertiesEventHub.Instance.sub_ManagePropertiesOpened(drop);
        ManagePropertiesEventHub.Instance.sub_BackButtonPressed(raise);
        UIEventHub.Instance.sub_EstateAddedBuilding(updateEstateVisual);
        UIEventHub.Instance.sub_EstateRemovedBuilding(updateEstateVisual);
    }
    #endregion



    #region public
    public PlayerInfo SelectedPlayer => selectedPlayer;
    public void setSelectedPlayer(PlayerInfo playerInfo) {
        selectedPlayer = playerInfo;
    }
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
    #endregion



    #region private
    private void drop() {
        int dropFrames = InterfaceConstants.FRAMES_FOR_MANAGE_PROPERTIES_DROP;
        IEnumerator dropCoroutine() {
            float length = offScreenY - onScreenY;
            for (int i = 1; i <= dropFrames; i++) {
                float yPos = LinearValue.exe(i, offScreenY, onScreenY, dropFrames);
                rt.anchoredPosition = new Vector2(0f, yPos);
                yield return null;
            }
            rt.anchoredPosition = new Vector2(0f, onScreenY);
            backButton.interactable = true;
        }
        void setIcons() {
            ManagePropertiesTokenIcon getComponent(int i) {
                return tokenIconContainerRT.GetChild(i).GetChild(0).GetComponent<ManagePropertiesTokenIcon>();
            }

            IEnumerable<PlayerInfo> activePlayers = GameState.game.ActivePlayers;
            for (int i = 0; i < GameConstants.MAX_PLAYERS; i++) {
                if (i < activePlayers.Count()) {
                    PlayerInfo activePlayer = activePlayers.ElementAt(i);
                    ManagePropertiesTokenIcon managePropertiesTokenIcon = getComponent(i);
                    managePropertiesTokenIcon.setup(activePlayer);
                    if (activePlayer == GameState.game.TurnPlayer) managePropertiesTokenIcon.select(true);
                }
                else tokenIconContainerRT.GetChild(i).gameObject.SetActive(false);
            }
        }

        setIcons();
        screenCover.startFadeIn(255f);
        StartCoroutine(dropCoroutine());
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(GameState.game.TurnPlayer);
    }
    private void raise() {
        int raiseFrames = InterfaceConstants.FRAMES_FOR_MANAGE_PROPERTIES_DROP;
        IEnumerator raiseCoroutine() {
            float length = offScreenY - onScreenY;
            for (int i = 1; i <= raiseFrames; i++) {
                float yPos = LinearValue.exe(i, onScreenY, offScreenY, raiseFrames);
                rt.anchoredPosition = new Vector2(0f, yPos);
                yield return null;
            }
            rt.anchoredPosition = new Vector2(0f, offScreenY);
        }

        screenCover.startFadeOut();
        StartCoroutine(raiseCoroutine());
    }
    private void updateEstateVisual(EstateInfo estateInfo) {
        EstateSection estateSection = estateSectionGetter.exe(estateInfo);
        estateSection.updateBuildingIcons();
    }
    #endregion
}
