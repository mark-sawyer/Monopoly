using UnityEngine;

[CreateAssetMenu(fileName = "CardInstance", menuName = "Card/CardInstance")]
internal class CardInstance : ScriptableObject {
    [SerializeField] private CardType cardType;
    [SerializeField] private CardMechanic cardMechanic;
    [SerializeField] private int id;



    #region CardInfo
    public int ID => id;
    public CardType CardType => cardType;
    #endregion
}

public interface CardInfo {
    public int ID { get; }
    public CardType CardType { get; }
}
