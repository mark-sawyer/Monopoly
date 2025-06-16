using System.Collections;
using UnityEngine;

public class TokenScaler : MonoBehaviour {
    #region public
    public PlayerInfo PlayerInfo { get; private set; }
    public void setup(PlayerInfo player, float scale) {
        PlayerInfo = player;
        transform.localScale = new Vector3(scale, scale, scale);
    }
    public void beginScaleChange(float targetScale) {
        StartCoroutine(changeScale(targetScale));
    }
    #endregion



    #region private
    private IEnumerator changeScale(float targetScale) {
        float startScale = transform.localScale.x;
        int frames = InterfaceConstants.FRAMES_FOR_TOKEN_GROWING;
        float slope = (targetScale - startScale) / frames;
        for (int i = 1; i <= frames; i++) {
            float scale = startScale + slope * i;
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }
    #endregion
}
