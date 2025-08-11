using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : ScreenOverlay<GameObject> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject okButton;
    private GameObject cardPrefab;
    private GameObject cardInstance;
    private ScreenOverlaySizeAdjuster screenOverlaySizeAdjuster;
    private const float HORIZONTAL_PROPORTION = 710f / 1920f;




    #region ScreenAnimation
    public override void setup(GameObject cardPrefab) {
        this.cardPrefab = cardPrefab;
        ScreenOverlayEventHub.Instance.sub_CardOKClicked(
            () => ScreenOverlayEventHub.Instance.call_RemoveScreenAnimation()
        );
        screenOverlaySizeAdjuster = new ScreenOverlaySizeAdjuster(
            HORIZONTAL_PROPORTION,
            ((RectTransform)cardPrefab.transform).rect.width,
            rt
        );
        screenOverlaySizeAdjuster.adjustChildrenSize();
    }
    public override void appear() {
        float canvasHeight = rt.rect.height;
        float canvasWidth = rt.rect.width;
        cardInstance = screenOverlaySizeAdjuster.InstantiateAdjusted(cardPrefab);
        cardInstance.transform.localPosition = new Vector3(
            -canvasWidth / 4f,
            -1.1f * canvasHeight / 2f,
            0f
        );
        cardInstance.transform.localRotation = Quaternion.Euler(-100f, 20, 0);
        cardInstance.GetComponent<CardMonoBehaviour>().startCoroutines();        
        okButton.GetComponent<OKButton>().raise(((RectTransform)cardPrefab.transform).rect.height * screenOverlaySizeAdjuster.Scale);
    }
    #endregion
}
