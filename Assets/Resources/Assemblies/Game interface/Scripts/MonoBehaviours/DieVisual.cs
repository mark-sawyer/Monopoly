using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DieVisual : MonoBehaviour {
    [SerializeField] private Button rollButton;
    [SerializeField] private Image image;
    private Sprite[] settledSprites;
    private Sprite[] rollingSprites;
    private DieValueReader die;



    /* public */
    public void setDie(DieValueReader die) {
        this.die = die;
    }



    /* MonoBehaviour */
    private void Start() {
        rollButton.onClick.AddListener(updateSprite);
        settledSprites = getSettledSprites();
        rollingSprites = getRollingSprites();
    }



    /* private */
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
        image.sprite = settledSprites[die.getValue() - 1];
    }
    private void updateSprite() {
        StartCoroutine(rollDieAnimation());
    }
    private Sprite[] getSettledSprites() {
        return new Sprite[] {
            Resources.Load<Sprite>("Sprites/Dice/Settled dice/one"),
            Resources.Load<Sprite>("Sprites/Dice/Settled dice/two"),
            Resources.Load<Sprite>("Sprites/Dice/Settled dice/three"),
            Resources.Load<Sprite>("Sprites/Dice/Settled dice/four"),
            Resources.Load<Sprite>("Sprites/Dice/Settled dice/five"),
            Resources.Load<Sprite>("Sprites/Dice/Settled dice/six")
        };
    }
    private Sprite[] getRollingSprites() {
        return new Sprite[] {
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_1"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_2"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_3"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_4"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_5"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_6"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_7"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_8"),
            Resources.Load<Sprite>("Sprites/Dice/Rolling dice/roll_9")
        };
    }
}
