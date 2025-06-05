using UnityEngine;
using UnityEngine.UI;

public class JailUIAlternator : MonoBehaviour {
    [SerializeField] private Transform doublesTransform;
    [SerializeField] private Transform payButtonTransform;
    [SerializeField] private Transform cardButtonTransform;



    #region public
    public void buttonsVisible() {
        buttonsToggle(true);
    }
    public void checkBoxesVisible() {
        buttonsToggle(false);
    }
    #endregion



    #region public
    private void toggleCheckboxes() {

    }
    private void buttonsToggle(bool toggle) {
        void buttonImagesToggle(Transform buttonTransform, bool toggle) {
            Transform[] panelTransforms = new Transform[3] {
                buttonTransform.GetChild(0),             // White background
                buttonTransform.GetChild(1),             // White outer
                buttonTransform.GetChild(1).GetChild(1)  // Red inner
            };
            foreach (Transform tf in panelTransforms) {
                toggleImages(tf, toggle);
            }
        }
        void toggleImages(Transform panelTransform, bool toggle) {
            Transform sections = panelTransform.GetChild(0);
            for (int i = 0; i < 9; i++) {
                Transform sectionChild = sections.GetChild(i);
                Image image = sectionChild.GetComponent<Image>();
                image.enabled = toggle;
            }
        }
        payButtonTransform.GetComponent<Button>().interactable = toggle && GameState.game.TurnPlayer.Money >= 50;
        payButtonTransform.GetComponent<Button>().interactable = toggle && GameState.game.TurnPlayer.Money >= 50;
        buttonImagesToggle(payButtonTransform, toggle);
        buttonImagesToggle(cardButtonTransform, toggle);
    }
    #endregion
}
