using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenScaler : MonoBehaviour {
    private const int FRAMES_FOR_TOKEN_GROWING = 50;



    #region MonoBehaviour
    void Start() {
        setStartingScale();
    }
    #endregion



    #region public
    public PlayerInfo PlayerInfo { get; private set; }
    public void setup(PlayerInfo player) {
        PlayerInfo = player;
    }
    public void beginScaleChange(float targetScale) {
        StartCoroutine(changeScale(targetScale));
    }
    public void beginScaleChange() {
        SpaceVisual spaceVisual = SpaceVisualManager.Instance.getSpaceVisual(PlayerInfo.SpaceIndex);
        StartCoroutine(changeScale(spaceVisual.getScale(PlayerInfo)));
    }
    #endregion



    #region private
    private IEnumerator changeScale(float targetScale) {
        float startScale = transform.localScale.x;
        int frames = FRAMES_FOR_TOKEN_GROWING;
        float slope = (targetScale - startScale) / frames;
        for (int i = 0; i < frames; i++) {
            float scale = startScale + slope * i;
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }
    private void setStartingScale() {
        float scale = SpaceVisualManager.Instance.getSpaceVisual(0).getScale(PlayerInfo);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    #endregion
}
