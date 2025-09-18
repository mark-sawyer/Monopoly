using UnityEngine;
using UnityEngine.UI;

public class UseCardButton : PrerollButton {
    [SerializeField] private CardType cardType;
    [SerializeField] private Image padlockImage;
    [SerializeField] private Transform colourPanelRT;



    #region MonoBehaviour
    private void Start() {
        CardColourDictionary cardColourDictionary = CardColourDictionary.Instance;
        Color panelColour = cardColourDictionary.lookupCardColour(cardType);
        Color padlockColour = cardColourDictionary.lookupPadlockColour(cardType);
        padlockImage.color = padlockColour;
        PanelRecolourer panelRecolourer = new PanelRecolourer(colourPanelRT);
        panelRecolourer.recolour(panelColour);
    }
    #endregion



    #region public
    public void buttonClicked() {
        DataUIPipelineEventHub.Instance.call_UseGOOJFCardButtonClicked(cardType);
    }
    #endregion



    #region PrerollButton
    protected override bool Interactable => GameState.game.TurnPlayer.hasGOOJFCardOfType(cardType);
    #endregion
}
