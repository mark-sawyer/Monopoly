using System.Collections;
using UnityEngine;

public class ManagePropertiesWipe : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    private const float MAX_HEIGHT = 600f;



    #region MonoBehaviour
    private void Start() {
        ManagePropertiesEventHub.Instance.sub_WipeToCommence(wipe);
    }
    #endregion



    #region MonoBehaviour
    public void wipe(PlayerInfo playerInfo) {
        StartCoroutine(wipeCoroutine(playerInfo));
    }
    private IEnumerator wipeCoroutine(PlayerInfo playerInfo) {
        float width = rt.rect.width;
        int wipeFrames = FrameConstants.MANAGE_PROPERTIES_WIPE_UP;
        for (int i = 1; i <= FrameConstants.MANAGE_PROPERTIES_WIPE_UP; i++) {
            float height = LinearValue.exe(i, 0f, MAX_HEIGHT, wipeFrames);
            rt.sizeDelta = new Vector2(width, height);
            yield return null;
        }

        rt.sizeDelta = new Vector2(width, MAX_HEIGHT);
        bool regularRefresh = ManagePropertiesPanel.Instance.IsRegularRefreshMode;
        ManagePropertiesEventHub.Instance.call_ManagePropertiesVisualRefresh(playerInfo, regularRefresh);
        yield return null;

        for (int i = 1; i <= wipeFrames; i++) {
            float height = LinearValue.exe(i, MAX_HEIGHT, 0f, wipeFrames);
            rt.sizeDelta = new Vector2(width, height);
            yield return null;
        }
        rt.sizeDelta = new Vector2(width, 0f);
    }
    #endregion
}
