using UnityEngine;

internal abstract class Card : ScriptableObject {
    [SerializeField] private CardType cardType;

    internal abstract void execute();
}
