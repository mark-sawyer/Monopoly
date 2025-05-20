using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DieVisual : MonoBehaviour {
    [SerializeField] private int dieIndex;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] settledSprites;
    [SerializeField] private Sprite[] rollingSprites;
    private UnityEvent animationOver = new UnityEvent();
    private DiceInfo dice;



    #region Public
    public void listenForAnimationOver(UnityAction a) {
        animationOver.AddListener(a);
    }
    public void startDieRoll() {
        StartCoroutine(rollDieAnimation());
    }
    #endregion



    #region MonoBehaviour
    private void Start() {
        dice = GameState.game.DiceInfo;
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
        animationOver.Invoke();
        image.sprite = settledSprites[dice.getDieValue(dieIndex) - 1];
    }
    #endregion
}
