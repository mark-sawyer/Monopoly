using UnityEngine;

[CreateAssetMenu(fileName = "New card space", menuName = "Space/CardSpace")]
internal class CardSpace : Space, CardSpaceInfo {
    [SerializeField] private CardType cardType;
    public CardType CardType => cardType;
}

public interface CardSpaceInfo {
    public CardType CardType { get; }
}
