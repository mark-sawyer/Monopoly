using UnityEngine;
using System.Collections.Generic;

public class GameFactory {
    public GamePlayer GamePlayer => game;
    public GameStateInfo GameStateInfo => game;
    private Game game;



    #region Factory
    public void makeGame(int playerNum) {
        Queue<CardInstance> communityChestCards = initialiseCards(CardType.COMMUNITY_CHEST);
        Queue<CardInstance> chanceCards = initialiseCards(CardType.CHANCE);
        game = new Game(
            playerNum,
            GameConstants.STARTING_MONEY,
            new Dice(),
            communityChestCards,
            chanceCards
        );
        setGameRefForCards(communityChestCards, game);
        setGameRefForCards(chanceCards, game);
    }
    public void makeTestGame(int playerNum, int startingMoney) {
        Queue<CardInstance> communityChestCards = initialiseTestCards(CardType.COMMUNITY_CHEST);
        Queue<CardInstance> chanceCards = initialiseTestCards(CardType.CHANCE);
        game = new Game(
            playerNum,
            startingMoney,
            new RiggedDice(),
            communityChestCards,
            chanceCards
        );
        setGameRefForCards(communityChestCards, game);
        setGameRefForCards(chanceCards, game);
    }
    #endregion



    #region private
    private Queue<CardInstance> initialiseCards(CardType cardType) {
        Deck deck = cardType == CardType.COMMUNITY_CHEST
            ? Resources.Load<Deck>("ScriptableObjects/Cards/cc_deck")
            : Resources.Load<Deck>("ScriptableObjects/Cards/chance_deck");
        return deck.getAsQueue();
    }
    private Queue<CardInstance> initialiseTestCards(CardType cardType) {
        Deck deck = cardType == CardType.COMMUNITY_CHEST
            ? Resources.Load<Deck>("ScriptableObjects/Cards/cc_test_deck")
            : Resources.Load<Deck>("ScriptableObjects/Cards/chance_test_deck");
        return deck.getAsQueue();
    }
    private void setGameRefForCards(Queue<CardInstance> queue, Game game) {
        foreach (CardInstance card in queue) {
            card.setup(game);
        }
    }
    #endregion
}
