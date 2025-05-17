using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenScaler : MonoBehaviour {
    public PlayerInfo player { get; private set; }
    private SpaceVisualManager spaceVisualManager;
    private const int FRAMES_FOR_TOKEN_GROWING = 50;



    #region MonoBehaviour
    void Start() {
        setStartingScale();
    }
    #endregion



    #region public
    public void setup(PlayerInfo player, SpaceVisualManager spaceVisualManager) {
        this.player = player;
        this.spaceVisualManager = spaceVisualManager;
    }
    public void beginScaleChange(float targetScale) {
        StartCoroutine(changeScale(targetScale));
    }
    public void beginScaleChange() {
        SpaceVisual spaceVisual = spaceVisualManager.getSpaceVisual(player.SpaceIndex);
        StartCoroutine(changeScale(spaceVisual.getScale()));
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
        float scale = spaceVisualManager.getSpaceVisual(0).getScale();
        transform.localScale = new Vector3(scale, scale, scale);
    }
    #endregion
}
