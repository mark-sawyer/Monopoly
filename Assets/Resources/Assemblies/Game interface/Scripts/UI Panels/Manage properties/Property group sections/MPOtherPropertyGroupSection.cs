using UnityEngine;

public class MPOtherPropertyGroupSection : MPPropertyGroupSection {
    [SerializeField] private GameColour otherPropertyGroupTransitionPanelColour;

    public override Color TransitionPanelColour => otherPropertyGroupTransitionPanelColour.Colour;
}
