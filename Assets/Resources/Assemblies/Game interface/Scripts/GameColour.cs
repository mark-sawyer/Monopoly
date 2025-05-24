using UnityEngine;

[CreateAssetMenu(fileName = "new GameColour", menuName = "GameColour")]
public class GameColour : ScriptableObject {
    [SerializeField] private Color colour;
    public Color Colour => colour;
}
