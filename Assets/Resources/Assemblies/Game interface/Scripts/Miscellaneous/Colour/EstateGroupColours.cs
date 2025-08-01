using UnityEngine;

[CreateAssetMenu(fileName = "new GameColour", menuName = "Colour/EstateGroupColours")]
public class EstateGroupColours : ScriptableObject {
    [SerializeField] private GameColour mainColour;
    [SerializeField] private GameColour highlightColour;
    [SerializeField] private GameColour backgroundColour;
    [SerializeField] private GameColour panelColour;



    public GameColour MainColour => mainColour;
    public GameColour HighlightColour => highlightColour;
    public GameColour BackgroundColour => backgroundColour;
    public GameColour PanelColour => panelColour;
}
