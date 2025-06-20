using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : ScreenAnimation<GameObject> {
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameEvent okButtonClicked;
    private GameObject cardPrefab;
    private GameObject cardInstance;
    private ScreenAnimationSizeAdjuster screenAnimationSizeAdjuster;
    private const float HORIZONTAL_PROPORTION = 710f / 1920f;




    #region ScreenAnimation
    public override void setup(GameObject cardPrefab) {
        this.cardPrefab = cardPrefab;
        okButtonClicked.Listeners += () => removeScreenAnimation.invoke();
        screenAnimationSizeAdjuster = new ScreenAnimationSizeAdjuster(
            HORIZONTAL_PROPORTION,
            ((RectTransform)cardPrefab.transform).rect.width,
            rt
        );
        screenAnimationSizeAdjuster.adjustChildrenSize();
    }
    public override void appear() {
        float canvasHeight = rt.rect.height;
        float canvasWidth = rt.rect.width;
        cardInstance = screenAnimationSizeAdjuster.InstantiateAdjusted(cardPrefab);
        cardInstance.transform.localPosition = new Vector3(
            -canvasWidth / 4f,
            -1.1f * canvasHeight / 2f,
            0f
        );
        cardInstance.transform.localRotation = Quaternion.Euler(-100f, 20, 0);
        cardInstance.GetComponent<CardMonoBehaviour>().startCoroutines();        
        okButton.GetComponent<OKButton>().raise(((RectTransform)cardPrefab.transform).rect.height * screenAnimationSizeAdjuster.Scale);
    }
    #endregion
}
