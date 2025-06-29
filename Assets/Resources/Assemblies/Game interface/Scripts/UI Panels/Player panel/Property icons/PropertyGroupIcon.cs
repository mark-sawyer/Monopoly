using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PropertyGroupIcon : MonoBehaviour {
    [SerializeField] Canvas canvas;
    [SerializeField] private SoundEvent propertyAdjustmentPop;
    private PlayerInfo playerInfo;
    private const float ZERO_PROPERTIES_ALPHA = 0.125f;
    private const float NON_ZERO_PROPERTIES_ALPHA = 1f;



    #region public
    public PlayerInfo PlayerInfo => playerInfo;
    public void setup(PlayerInfo playerInfo) {
        this.playerInfo = playerInfo;
    }
    public abstract void updateVisual();
    public abstract bool iconNeedsToUpdate();
    public abstract void setNewState();
    public IEnumerator pulseAndUpdate() {
        float getScale(float x) {
            if (x <= 5) return 1f + 0.2f * x;
            else return 2f - (1f / 15f) * (x - 5f);
        }

        propertyAdjustmentPop.play();
        for (int i = 0; i < 10; i++) yield return null;

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
    }
    #endregion



    #region protected
    protected void updatePanelColour(Color colour) {
        for (int i = 0; i < 9; i++) {
            transform.GetChild(0).GetChild(i).GetComponent<Image>().color = colour;
        }
    }
    protected float ZeroPropertiesAlpha => ZERO_PROPERTIES_ALPHA;
    protected float NonZeroPropertiesAlpha => NON_ZERO_PROPERTIES_ALPHA;
    #endregion
}
