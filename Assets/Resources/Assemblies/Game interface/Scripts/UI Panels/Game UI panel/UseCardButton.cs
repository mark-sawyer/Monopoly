using UnityEngine;
using UnityEngine.UI;

public class UseCardButton : MonoBehaviour {
    [SerializeField] private CardType cardType;
    [SerializeField] private Image padlockImage;
    [SerializeField] private Transform colourSectionsTransform;
    #region GameColours
    [SerializeField] private GameColour chanceColour;
    [SerializeField] private GameColour communityChestColour;
    [SerializeField] private GameColour chancePadlockColour;
    [SerializeField] private GameColour communityChestPadlockColour;
    #endregion



    #region MonoBehaviour
    private void Start() {
        if (cardType == CardType.CHANCE) changeColour(chanceColour.Colour, chancePadlockColour.Colour);
        else changeColour(communityChestColour.Colour, communityChestPadlockColour.Colour);
    }
    #endregion



    #region public
    public void buttonClicked() {
        DataUIPipelineEventHub.Instance.call_UseGOOJFCardButtonClicked(cardType);
    }
    #endregion



    #region private
    private void changeColour(Color panelColour, Color padlockColour) {
        padlockImage.color = padlockColour;
        for (int i = 0; i < colourSectionsTransform.childCount; i++) {
            colourSectionsTransform.GetChild(i).GetComponent<Image>().color = panelColour;
        }
    }
    #endregion
}
