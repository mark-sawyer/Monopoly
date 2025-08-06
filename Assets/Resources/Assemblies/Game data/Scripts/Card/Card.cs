using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Card")]
internal class Card : ScriptableObject, CardInfo, Tradable {
    [SerializeField] private CardType cardType;
    [SerializeField] private CardMechanic cardMechanic;
    [SerializeField] private int id;



    #region internal
    internal void setup(Game game) {
        cardMechanic.setup(game);
    }
    internal CardMechanic CardMechanic => cardMechanic;
    #endregion



    #region CardInfo
    public int ID => id;
    public CardType CardType => cardType;
    public CardMechanicInfo CardMechanicInfo => cardMechanic;
    #endregion



    #region Tradable
    public int TradableOrderID => cardType == CardType.COMMUNITY_CHEST ? 29 : 30;
    public string Abbreviation => "CARD";
    public void giveFromOneToTwo(Player playerOne, Player playerTwo) {
        playerOne.handBackGOOJFCard(cardType);
        playerTwo.getGOOJFCard(this);
    }
    #endregion
}

public interface CardInfo {
    public int ID { get; }
    public CardType CardType { get; }
    public CardMechanicInfo CardMechanicInfo { get; }
}
