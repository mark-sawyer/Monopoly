using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagePropertiesPanel : MonoBehaviour {
    #region Internal references
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform tokenIconContainerRT;
    #endregion
    #region External references
    [SerializeField] private GameEvent managePropertiesOpened;
    [SerializeField] private PlayerEvent managePropertiesVisualRefresh;
    [SerializeField] private ScreenCover screenCover;
    #endregion
    #region Private attributes
    private float offScreenY;
    private float onScreenY;
    #endregion
    #region Numeric constants
    private const float VERTICAL_PROPORTION = 800f / 1080f;
    private const float HEIGHT_ABOVE_CANVAS_PROPORTION = 5f / 36f;
    private const int FRAMES_TO_DROP = 20;
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
        managePropertiesOpened.Listeners += drop;
    }
    #endregion



    #region private
    private void drop() {
        IEnumerator dropCoroutine() {
            float length = offScreenY - onScreenY;
            for (int i = 1; i <= FRAMES_TO_DROP; i++) {
                float yPos = LinearValue.exe(i, offScreenY, onScreenY, FRAMES_TO_DROP);
                rt.anchoredPosition = new Vector2(0f, yPos);
                yield return null;
            }
            rt.anchoredPosition = new Vector2(0f, onScreenY);
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
        screenCover.startFadeIn();
        StartCoroutine(dropCoroutine());
        managePropertiesVisualRefresh.invoke(GameState.game.TurnPlayer);
    }
    #endregion
}
