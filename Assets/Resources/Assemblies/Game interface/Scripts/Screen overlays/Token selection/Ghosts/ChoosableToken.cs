using UnityEngine;

public class ChoosableToken : DraggableGhostSource {
    [SerializeField] private Token token;
    public Token Token => token;
}
