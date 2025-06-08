using UnityEngine;

[CreateAssetMenu(menuName = "State/DrawCardState")]
public class DrawCardState : State {
    [SerializeField] private GameEvent<CardType> cardDrawn;
    [SerializeField] private GameEvent okButtonClicked;
    private bool okClicked;



    #region GameState
    public override void enterState() {
        okClicked = false;
        okButtonClicked.Listeners += okButtonListener;
        PlayerInfo playerInfo = GameState.game.TurnPlayer;
        CardSpaceInfo cardSpaceInfo = (CardSpaceInfo)playerInfo.SpaceInfo;
        CardType cardType = cardSpaceInfo.CardType;
        cardDrawn.invoke(cardType);
    }
    public override bool exitConditionMet() {
        return okClicked;
    }
    public override void exitState() {
        okButtonClicked.Listeners -= okButtonListener;
    }
    public override State getNextState() {
        CardMechanicInfo cardMechanicInfo = RevealedCard.card.CardMechanicInfo;
        if (cardMechanicInfo is AdvanceToCardInfo) return getState<AdvanceToState>();
        if (cardMechanicInfo is BackThreeSpacesCardInfo) return getState<BackThreeState>();
        throw new System.Exception();
    }
    #endregion



    private void okButtonListener() {
        okClicked = true;
    }
}
