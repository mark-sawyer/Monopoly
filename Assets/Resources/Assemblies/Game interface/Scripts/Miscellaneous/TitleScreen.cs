using UnityEngine;

public class TitleScreen : MonoBehaviour {
    #region MonoBehaviour
    private void Start() {
        UIEventHub.Instance.sub_StartGameClicked(startClicked);
    }
    #endregion



    #region public
    public void startClicked() {
        ScreenOverlayStarterEventHub.Instance.call_PlayerNumberSelection();
        Destroy(gameObject);
    }
    public void quitClicked() {
        Application.Quit();
    }
    #endregion
}
