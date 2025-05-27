using UnityEngine;

[CreateAssetMenu(fileName = "new TokenColours", menuName = "Token/TokenColours")]
public class TokenColours : ScriptableObject {
    public Color OutlineColour { get => outlineColour; }
    public Color TokenColour { get => tokenColour; }
    public Color InnerCircleColour { get => innerCircleColour; }
    public Color OuterCircleColour { get => outerCircleColour; }
    [SerializeField] private Color outlineColour;
    [SerializeField] private Color tokenColour;
    [SerializeField] private Color innerCircleColour;
    [SerializeField] private Color outerCircleColour;
}
