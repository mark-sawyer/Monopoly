using UnityEngine;

[CreateAssetMenu(fileName = "New card space", menuName = "Space/CardSpace")]
internal class CardSpace : Space {
    [SerializeField] private CardType cardType;
}
