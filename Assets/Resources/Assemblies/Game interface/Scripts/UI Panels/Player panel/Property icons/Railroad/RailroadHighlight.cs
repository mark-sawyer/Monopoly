using UnityEngine;
using UnityEngine.UI;

public class RailroadHighlight : MonoBehaviour {
    [SerializeField] private Image image;
    [SerializeField] private GameObject mortgageSquarePanel;
    [SerializeField] private GameColour mortgageColour;
    [SerializeField] private GameColour railroadHighlightColour;
    [SerializeField] private ScriptableObject railroadGroupSO;
    private RailroadInfo railroadInfo;
    private RailroadGroupInfo railroadGroupInfo;
    private PlayerInfo playerInfo;
    private Color noColour;



    #region public
    public void setup(RailroadInfo railroadInfo, PlayerInfo playerInfo) {
        this.railroadInfo = railroadInfo;
        this.playerInfo = playerInfo;
        railroadGroupInfo = (RailroadGroupInfo)railroadGroupSO;
        noColour = new Color(0f, 0f, 0f, 0f);
    }
    public void setHighlight() {
        if (railroadInfo.Owner != playerInfo)
            notOwnedByPlayer();
        else if (railroadInfo.IsMortgaged)
            railroadIsMortgaged();
        else
            ownedAndUnmortgaged();
    }
    #endregion



    #region private
    private void notOwnedByPlayer() {
        mortgageSquarePanel.SetActive(false);
        image.color = noColour;
    }
    private void railroadIsMortgaged() {
        if (railroadGroupInfo.MortgageCount == railroadGroupInfo.NumberOfPropertiesInGroup) {
            mortgageSquarePanel.SetActive(false);
            image.color = noColour;
        }
        else {
            mortgageSquarePanel.SetActive(true);
            image.color = mortgageColour.Colour;
        }
    }
    private void ownedAndUnmortgaged() {
        mortgageSquarePanel.SetActive(false);

        int railroadsOwnedByPlayer = railroadGroupInfo.propertiesOwnedByPlayer(playerInfo);
        if (railroadsOwnedByPlayer == 4) image.color = noColour;
        else image.color = railroadHighlightColour.Colour;
    }
    #endregion
}
