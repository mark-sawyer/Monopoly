using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DieVisual : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] settledSprites;
    [SerializeField] private Sprite[] rollingSprites;
    private int framesPerImage;
    private int imagesBeforeSettling;



    #region MonoBehaviour
    private void Start() {
        framesPerImage = InterfaceConstants.DIE_FRAMES_PER_IMAGE;
        imagesBeforeSettling = InterfaceConstants.DIE_IMAGES_BEFORE_SETTLING;
}
    #endregion



    #region public
    public void startDieRoll(int dieValue) {
        StartCoroutine(rollDieAnimation(dieValue));
    }
    #endregion



    #region private
    private IEnumerator rollDieAnimation(int dieValue) {
        int lastRoll = -1;
        for (int i = 0; i < framesPerImage * imagesBeforeSettling; i++) {
            if (i % framesPerImage == 0) {
                int index = Random.Range(0, InterfaceConstants.NUMBER_OF_ROLLING_SPRITES);
                if (index == lastRoll) {
                    index = (index + 1) % InterfaceConstants.NUMBER_OF_ROLLING_SPRITES;
                }
                lastRoll = index;
                image.sprite = rollingSprites[index];
            }
            yield return null;
        }
        image.sprite = settledSprites[dieValue - 1];
    }
    #endregion
}
