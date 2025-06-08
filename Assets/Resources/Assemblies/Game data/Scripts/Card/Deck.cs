using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Card/Deck")]
internal class Deck : ScriptableObject {
    [SerializeField] private List<CardInstance> cards;

    internal Queue<CardInstance> getAsQueue() {
        randomiseCards();
        Queue<CardInstance> cardQueue = new Queue<CardInstance>(cards.Count);
        for (int i = 0; i < cards.Count; i++) {
            cardQueue.Enqueue(cards[i]);
        }
        return cardQueue;
    }

    private void randomiseCards() {
        for (int i = 0; i < cards.Count - 1; i++) {
            int swapIndex = Random.Range(i, cards.Count);
            CardInstance hold = cards[i];
            cards[i] = cards[swapIndex];
            cards[swapIndex] = hold;
        }
    }
}
