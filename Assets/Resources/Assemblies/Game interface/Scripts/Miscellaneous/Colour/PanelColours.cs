using UnityEngine;

[CreateAssetMenu(menuName = "Colour/PanelColours")]
public class PanelColours : ScriptableObject {
    private static PanelColours instance;
    [SerializeField] private GameColour innerColour;
    [SerializeField] private GameColour outerColour;
    [SerializeField] private GameColour deadInnerColour;
    [SerializeField] private GameColour deadOuterColour;



    public static PanelColours Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<PanelColours>(
                    "ScriptableObjects/Colours/panel_colours"
                );
            }
            return instance;
        }
    }
    public GameColour InnerColour => innerColour;
    public GameColour OuterColour => outerColour;
    public GameColour DeadInnerColour => deadInnerColour;
    public GameColour DeadOuterColour => deadOuterColour;
}
