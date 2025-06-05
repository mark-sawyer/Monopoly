using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DieVisual : MonoBehaviour {
    [SerializeField] private int dieIndex;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] settledSprites;
    [SerializeField] private Sprite[] rollingSprites;



    #region Public
    public void startDieRoll(int framesPerImage, int imagesBeforeSettling) {
        StartCoroutine(rollDieAnimation(framesPerImage, imagesBeforeSettling));
    }
    #endregion



    #region private
    private IEnumerator rollDieAnimation(int framesPerImage, int imagesBeforeSettling) {
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
        image.sprite = settledSprites[GameState.game.DiceInfo.getDieValue(dieIndex) - 1];
    }
    #endregion
}
