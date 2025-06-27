using UnityEngine;

public class OtherPropertyGroupSection : PropertyGroupSection {
    [SerializeField] private GameColour otherPropertyGroupTransitionPanelColour;

    public override Color TransitionPanelColour => otherPropertyGroupTransitionPanelColour.Colour;
}
