using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Card/Deck")]
internal class Deck : ScriptableObject {
    [SerializeField] private List<Card> cards;

    internal Queue<Card> getAsQueue() {
        randomiseCards();
        Queue<Card> cardQueue = new Queue<Card>(cards.Count);
        for (int i = 0; i < cards.Count; i++) {
            cardQueue.Enqueue(cards[i]);
        }
        return cardQueue;
    }

    private void randomiseCards() {
        for (int i = 0; i < cards.Count - 1; i++) {
            int swapIndex = Random.Range(i, cards.Count);
            Card hold = cards[i];
            cards[i] = cards[swapIndex];
            cards[swapIndex] = hold;
        }
    }
}
