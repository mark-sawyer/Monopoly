using System.Collections;
using UnityEngine;

public class ManagePropertiesWipe : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    private const float MAX_HEIGHT = 600f;
    private bool wipeInProgress;



    #region Singleton boilerplate
    public static ManagePropertiesWipe Instance { get; private set; }
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
        ManagePropertiesEventHub.Instance.sub_WipeToCommence(wipe);
        wipeInProgress = false;
    }
    #endregion



    #region public
    public bool WipeInProgress => wipeInProgress;
    public void wipe(PlayerInfo playerInfo) {
        StartCoroutine(wipeCoroutine(playerInfo));
    }
    #endregion



    #region private
    private IEnumerator wipeCoroutine(PlayerInfo playerInfo) {
        ManagePropertiesEventHub.Instance.call_PanelPaused();

        wipeInProgress = true;
        float width = rt.rect.width;
        int wipeFrames = FrameConstants.MANAGE_PROPERTIES_WIPE_UP;
        SoundPlayer.Instance.play_Wipe();
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_WIPE_UP; i++) {
            float height = LinearValue.exe(i, 0f, MAX_HEIGHT, wipeFrames);
            rt.sizeDelta = new Vector2(width, height);
            yield return null;
        }

        rt.sizeDelta = new Vector2(width, MAX_HEIGHT);
        bool regularRefresh = ManagePropertiesPanel.Instance.IsRegularRefreshMode;
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(playerInfo, regularRefresh);
        yield return null;

        SoundPlayer.Instance.play_Wipe();
        for (int i = 1; i <= wipeFrames; i++) {
            float height = LinearValue.exe(i, MAX_HEIGHT, 0f, wipeFrames);
            rt.sizeDelta = new Vector2(width, height);
            yield return null;
        }
        rt.sizeDelta = new Vector2(width, 0f);
        wipeInProgress = false;

        ManagePropertiesEventHub.Instance.call_PanelUnpaused();
    }
    #endregion
}
