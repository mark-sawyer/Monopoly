using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DieVisual : MonoBehaviour {
    [SerializeField] private int dieIndex;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] settledSprites;
    [SerializeField] private Sprite[] rollingSprites;
    [SerializeField] private GameEvent animationOver;



    #region Public
    //public void listenForAnimationOver(UnityAction a) {
    //    animationOver.AddListener(a);
    //}
    public void startDieRoll() {
        StartCoroutine(rollDieAnimation());
    }
    #endregion



    #region private
    private IEnumerator rollDieAnimation() {
        int lastRoll = -1;
        int framesPerImage = 10;
        int imagesBeforeSettling = 7;
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
        if (dieIndex == 0) animationOver.invoke();
        image.sprite = settledSprites[GameState.game.DiceInfo.getDieValue(dieIndex) - 1];
    }
    #endregion
}
