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
    [SerializeField] private PlayerEvent tokenSelectedInManageProperties;
    #endregion
    private PlayerInfo playerInfo;
    private float goalScale;
    private bool selected;
    #region Numeric constants
    private const float SELECTED_SCALE = 1.16f;
    private const float HOVER_COLOUR = 75f / 255f;
    private const float UNSELECTED_COLOUR = 150f / 255f;
    #endregion



    #region MonoBehaviour
    private void OnEnable() {
        tokenSelectedInManageProperties.Listeners += deselect;
    }
    private void OnDisable() {
        tokenSelectedInManageProperties.Listeners -= deselect;
    }
    private void Start() {
        goalScale = 1f;
        selected = false;
    }
    private void Update() {
        if (selected) return;

        float currentScale = transform.parent.localScale.x;
        float difference = goalScale - currentScale;
        float newScale = currentScale + difference / 20f;
        transform.parent.localScale = new Vector3(newScale, newScale, newScale);
    }
    #endregion



    #region Interfaces
    public void OnPointerEnter(PointerEventData eventData) {
        if (selected) return;

        setAlphaOfCover(HOVER_COLOUR);
        goalScale = SELECTED_SCALE;
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (selected) return;

        setAlphaOfCover(UNSELECTED_COLOUR);
        goalScale = 1f;
    }
    public void OnPointerClick(PointerEventData eventData) {
        if (selected) return;

        select();
    }
    #endregion



    #region public
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
        tokenIcon.setup(playerInfo.Token, playerInfo.Colour);
    }
    public void select() {
        selected = true;
        canvas.sortingOrder = 2;
        setAlphaOfCover(0f);
        StartCoroutine(pulse());
        tokenSelectedInManageProperties.invoke(playerInfo);
    }
    #endregion



    #region private
    private void deselect(PlayerInfo selectedPlayer) {
        if (!selected || selectedPlayer == playerInfo) return;

        selected = false;
        canvas.sortingOrder = 1;
        goalScale = 1f;
        setAlphaOfCover(UNSELECTED_COLOUR);
    }
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
    #endregion
}
