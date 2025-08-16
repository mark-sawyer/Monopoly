using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SellOrMortgageBuildingButton : MonoBehaviour {
    protected enum ButtonMode {
        SELL,
        MORTGAGE
    }
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private GameObject sellGameObject;
    [SerializeField] private GameObject mortgageGameObject;
    [SerializeField] private Button button;
    private EstateInfo estateInfo;
    private ButtonMode buttonMode;



    #region public
    public void setup(EstateInfo estateInfo) {
        this.estateInfo = estateInfo;
    }
    public abstract void buttonClicked();
    public virtual void adjustToAppropriateOption() {
        if (estateInfo.BuildingCount > 0) {
            toggleMode(ButtonMode.SELL);
            button.interactable = estateInfo.CanRemoveBuilding;
        }
        else {
            toggleMode(ButtonMode.MORTGAGE);
            bool noBuildingsInGroup = !estateInfo.EstateGroupInfo.BuildingExists;
            bool propertyNotMortgaged = !estateInfo.IsMortgaged;
            button.interactable = noBuildingsInGroup && propertyNotMortgaged;
        }
    }
    #endregion



    #region protected
    protected EstateInfo EstateInfo => estateInfo;
    protected ButtonMode CurrentMode => buttonMode;
    protected Button Button => button;
    protected void toggleMode(ButtonMode mode) {
        buttonMode = mode;
        if (mode == ButtonMode.SELL) {
            sellGameObject.SetActive(true);
            mortgageGameObject.SetActive(false);
            if (estateInfo.HasHotel) sellText.text = "SELL HOTEL";
            else sellText.text = "SELL HOUSE";
        }
        else {
            sellGameObject.SetActive(false);
            mortgageGameObject.SetActive(true);
        }
    }
    #endregion
}
