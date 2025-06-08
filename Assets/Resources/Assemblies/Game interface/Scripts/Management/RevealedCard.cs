using UnityEngine;

public class RevealedCard : MonoBehaviour {
    public static CardInfo card;
    [SerializeField] private GameEvent<CardInfo> cardRevealed;
    [SerializeField] private GameEvent cardUnrevealed;



    private void Start() {
        cardRevealed.Listeners += updateRevealedCard;
        cardUnrevealed.Listeners += removeRevealedCard;
    }
    private void updateRevealedCard(CardInfo cardInfo) {
        card = cardInfo;
    }
    private void removeRevealedCard() {
        card = null;
    }
}
