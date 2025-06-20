using UnityEngine;

[CreateAssetMenu(fileName = "CardInstance", menuName = "Card/CardInstance")]
internal class CardInstance : ScriptableObject, CardInfo {
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
}

public interface CardInfo {
    public int ID { get; }
    public CardType CardType { get; }
    public CardMechanicInfo CardMechanicInfo { get; }
}
