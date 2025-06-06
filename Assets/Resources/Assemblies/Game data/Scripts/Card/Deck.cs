using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Card/Deck")]
internal class Deck : ScriptableObject {
    [SerializeField] private List<CardInstance> cards;
    private const int DECK_SIZE = 16;

    internal Queue<CardInstance> getAsQueue() {
        randomiseCards();
        Queue<CardInstance> cardQueue = new Queue<CardInstance>(DECK_SIZE);
        for (int i = 0; i < DECK_SIZE; i++) {
            cardQueue.Enqueue(cards[i]);
        }
        return cardQueue;
    }

    private void randomiseCards() {
        for (int i = 0; i < DECK_SIZE - 1; i++) {
            int swapIndex = Random.Range(i, DECK_SIZE);
            CardInstance hold = cards[i];
            cards[i] = cards[swapIndex];
            cards[swapIndex] = hold;
        }
    }
}
