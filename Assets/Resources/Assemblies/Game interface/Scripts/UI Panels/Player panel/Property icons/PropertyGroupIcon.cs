using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PropertyGroupIcon : MonoBehaviour {
    [SerializeField] Canvas canvas;
    private PlayerInfo playerInfo;
    private const float ZERO_PROPERTIES_ALPHA = 0.125f;
    private const float NON_ZERO_PROPERTIES_ALPHA = 1f;



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public abstract bool NeedsToUpdate { get; }
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
    }
    public IEnumerator pulseAndUpdate() {
        yield return WaitFrames.Instance.frames(10);

        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
        for (int i = 1; i <= 20; i++) {
            float scale = getScale(i);
            transform.localScale = new Vector3(scale, scale, scale);
            if (i == 5) updateVisual();
            yield return null;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        canvas.overrideSorting = false;
        canvas.sortingOrder = 0;
        setNewState();
    }
    public IEnumerator pulseAndUpdateWithPop() {
        UIEventHub.Instance.call_AppearingPop();
        yield return pulseAndUpdate();
    }
    #endregion



    #region protected
    protected abstract void setNewState();
    protected void updatePanelColour(Color colour) {
        for (int i = 0; i < 9; i++) {
            transform.GetChild(0).GetChild(i).GetComponent<Image>().color = colour;
        }
    }
    protected abstract void updateVisual();
    protected float ZeroPropertiesAlpha => ZERO_PROPERTIES_ALPHA;
    protected float NonZeroPropertiesAlpha => NON_ZERO_PROPERTIES_ALPHA;
    #endregion



    #region private
    private float getScale(float x) {
        if (x <= 5) return 1f + 0.2f * x;
        else return 2f - (1f / 15f) * (x - 5f);
    }
    #endregion
}
