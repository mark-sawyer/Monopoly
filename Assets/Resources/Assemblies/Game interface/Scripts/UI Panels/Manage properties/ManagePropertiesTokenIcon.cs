using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManagePropertiesTokenIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    #region Internal references
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private Image transparentCover;
    [SerializeField] private Canvas canvas;
    #endregion
    #region External references
    [SerializeField] private GameColour panelColour;
    #endregion
    #region Numeric constants
    private const float SELECTED_SCALE = 1.16f;
    private const float HOVER_COLOUR = 75f / 255f;
    private const float UNSELECTED_COLOUR = 150f / 255f;
    #endregion
    #region Private attributes
    private PlayerInfo playerInfo;
    private float goalScale;
    private static bool wipeInProgress;
    #endregion



    #region MonoBehaviour
    private void Start() {
        goalScale = 1f;
        wipeInProgress = false;
    }
    private void Update() {
        if (Selected) return;

        float currentScale = transform.parent.localScale.x;
        float difference = goalScale - currentScale;
        float newScale = currentScale + difference / 20f;
        transform.parent.localScale = new Vector3(newScale, newScale, newScale);
    }
    #endregion



    #region Interfaces
    public void OnPointerEnter(PointerEventData eventData) {
        if (Selected) return;

        setAlphaOfCover(HOVER_COLOUR);
        goalScale = SELECTED_SCALE;
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (Selected) return;

        setAlphaOfCover(UNSELECTED_COLOUR);
        goalScale = 1f;
    }
    public void OnPointerClick(PointerEventData eventData) {
        if (Selected || wipeInProgress) return;

        select(false);
    }
    #endregion



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
    }
    public void select(bool isFromOpen) {
        PlayerInfo priorSelectedPlayer = ManagePropertiesPanel.Instance.SelectedPlayer;
        if (priorSelectedPlayer != null) {
            ManagePropertiesTokenIcon priorIcon = ManagePropertiesPanel.Instance.getManagePropertiesTokenIcon(priorSelectedPlayer);
            priorIcon.deselect();
        }
        ManagePropertiesPanel.Instance.setSelectedPlayer(playerInfo);
        canvas.sortingOrder = 2;
        setAlphaOfCover(0f);
        StartCoroutine(pulse());
        if (!isFromOpen) {
            ManagePropertiesEventHub.Instance.call_TokenSelectedInManageProperties(playerInfo);
            wipeInProgress = true;
            WaitFrames.Instance.beforeAction(
                2 * InterfaceConstants.FRAMES_FOR_MANAGE_PROPERTIES_WIPE_UP + 2,
                () => wipeInProgress = false
            );
        }
    }
    public void deselect() {
        canvas.sortingOrder = 1;
        goalScale = 1f;
        setAlphaOfCover(UNSELECTED_COLOUR);
    }
    #endregion



    #region private
    private void setAlphaOfCover(float alpha) {
        Color colour = transparentCover.color;
        colour.a = alpha;
        transparentCover.color = colour;
    }
    private IEnumerator pulse() {
        float getScale(float x) {
            if (x <= 5) return LinearValue.exe(x, 0f, 5f, SELECTED_SCALE, 1.5f);
            else return LinearValue.exe(x, 5f, 20f, 1.5f, SELECTED_SCALE); ;
        }

        transform.parent.localScale = new Vector3(SELECTED_SCALE, SELECTED_SCALE, SELECTED_SCALE);
        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.parent.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        transform.parent.localScale = new Vector3(SELECTED_SCALE, SELECTED_SCALE, SELECTED_SCALE);
    }
    private bool Selected => ManagePropertiesPanel.Instance.SelectedPlayer == playerInfo;
    #endregion
}
