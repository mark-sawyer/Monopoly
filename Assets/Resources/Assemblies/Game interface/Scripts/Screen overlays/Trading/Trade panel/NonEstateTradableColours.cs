using UnityEngine;

[CreateAssetMenu(menuName = "Colour/NonEstateTradableColours")]
public class NonEstateTradableColours : ScriptableObject {
    [SerializeField] private GameColour railroadBackgroundColour;
    [SerializeField] private GameColour railroadTextBorderColour;
    [SerializeField] private GameColour electricityBackgroundColour;
    [SerializeField] private GameColour electricityTextBorderColour;
    [SerializeField] private GameColour waterBackgroundColour;
    [SerializeField] private GameColour waterTextBorderColour;
    [SerializeField] private GameColour chanceBackgroundColour;
    [SerializeField] private GameColour chanceTextBorderColour;
    [SerializeField] private GameColour communityChestBackgroundColour;
    [SerializeField] private GameColour communityChestTextBorderColour;
    private static NonEstateTradableColours instance;



    public static NonEstateTradableColours Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<NonEstateTradableColours>(
                    "ScriptableObjects/Colours/non_estate_tradable_colours"
                );
            }
            return instance;
        }
    }
    public Color RailroadBackgroundColour => railroadBackgroundColour.Colour;
    public Color RailroadTextBorderColour => railroadTextBorderColour.Colour;
    public Color ElectricityBackgroundColour => electricityBackgroundColour.Colour;
    public Color ElectricityTextBorderColour => electricityTextBorderColour.Colour;
    public Color WaterBackgroundColour => waterBackgroundColour.Colour;
    public Color WaterTextBorderColour => waterTextBorderColour.Colour;
    public Color ChanceBackgroundColour => chanceBackgroundColour.Colour;
    public Color ChanceTextBorderColour => chanceTextBorderColour.Colour;
    public Color CommunityChestBackgroundColour => communityChestBackgroundColour.Colour;
    public Color CommunityChestTextBorderColour => communityChestTextBorderColour.Colour;
}
