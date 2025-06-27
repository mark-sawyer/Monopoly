using UnityEngine;

[CreateAssetMenu(fileName = "new GameColour", menuName = "Colour/GameColour")]
public class GameColour : ScriptableObject {
    [SerializeField] private Color colour;
    public Color Colour => colour;
}
