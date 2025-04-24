using UnityEngine;

[CreateAssetMenu(fileName = "New card space", menuName = "CardSpace")]
internal class CardSpace : Space {
    [SerializeField] private CardType cardType;
}
