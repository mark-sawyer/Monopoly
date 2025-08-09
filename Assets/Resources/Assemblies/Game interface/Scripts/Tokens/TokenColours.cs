using UnityEngine;

[CreateAssetMenu(fileName = "new TokenColours", menuName = "Token/TokenColours")]
public class TokenColours : ScriptableObject {
    [SerializeField] private Color outlineColour;
    [SerializeField] private Color tokenColour;
    [SerializeField] private Color innerCircleColour;
    [SerializeField] private Color outerCircleColour;
    [SerializeField] private Color deadInnerColour;
    [SerializeField] private Color deadOuterColour;



    public Color OutlineColour => outlineColour;
    public Color TokenColour => tokenColour;
    public Color InnerCircleColour => innerCircleColour;
    public Color OuterCircleColour => outerCircleColour;
    public Color DeadInnerColour => deadInnerColour;
    public Color DeadOuterColour => deadOuterColour;
}
