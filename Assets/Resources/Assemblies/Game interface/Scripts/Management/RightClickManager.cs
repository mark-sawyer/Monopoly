using UnityEngine;

public class RightClickManager : MonoBehaviour {
    [SerializeField] private GameObject deedParent;
    [SerializeField] private EstateDeed estateDeed;
    [SerializeField] private RailroadDeed railroadDeed;
    [SerializeField] private UtilityDeed utilityDeed;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private GameObject mortgagedText;
    private bool canShowDeeds;
    private PropertyInfo currentlyShown;
    GameObject shownDeed;



    #region MonoBehaviour
    private void Start() {
        UIEventHub.Instance.sub_PrerollStateStarting(prerollCheckForToggleOn);
        UIEventHub.Instance.sub_PrerollStateEnding(toggleShowingDeedsOff);
        CameraEventHub.Instance.sub_RotationStarted(toggleShowingDeedsOff);
        CameraEventHub.Instance.sub_RotationFinished(toggleShowingDeedsOn);
    }
    private void Update() {
        if (!canShowDeeds) return;

        if (Input.GetMouseButton(1)) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider == null) {
                clearShownCheck();
                return;
            }

            PropertyShower propertyShower = hit.collider.GetComponent<PropertyShower>();
            if (propertyShower == null) {
                clearShownCheck();
                return;
            }

            PropertyInfo propertyInfo = propertyShower.PropertyInfo;
            showPropertyDeed(propertyInfo);
        }
        else {
            clearShownCheck();
        }
    }
    #endregion



    #region private
    private void prerollCheckForToggleOn() {
        CameraController cameraController = CameraController.Instance;
        if (!cameraController.AutoOn || !cameraController.NeedsToRotate) {
            canShowDeeds = true;
        }
    }
    private void toggleShowingDeedsOn() {
        canShowDeeds = true;
    }
    private void toggleShowingDeedsOff() {
        canShowDeeds = false;
    }
    private void showPropertyDeed(PropertyInfo propertyInfo) {
        void handleDeed() {
            if (propertyInfo is EstateInfo estateInfo) {
                estateDeed.setupCard(estateInfo);
                estateDeed.gameObject.SetActive(true);
                shownDeed = estateDeed.gameObject;
            }
            else if (propertyInfo is RailroadInfo railroadInfo) {
                railroadDeed.setupCard(railroadInfo);
                railroadDeed.gameObject.SetActive(true);
                shownDeed = railroadDeed.gameObject;
            }
            else {
                UtilityInfo utilityInfo = (UtilityInfo)propertyInfo;
                utilityDeed.setupCard(utilityInfo);
                utilityDeed.gameObject.SetActive(true);
                shownDeed = utilityDeed.gameObject;
            }
        }
        void handleToken() {
            if (propertyInfo.Owner == null) return;

            Token token = propertyInfo.Owner.Token;
            PlayerColour colour = propertyInfo.Owner.Colour;
            tokenIcon.gameObject.SetActive(true);
            tokenIcon.setup(token, colour);
        }
        void handleMortgagedText() {
            if (propertyInfo.IsMortgaged) {
                mortgagedText.SetActive(true);
            }
        }


        if (propertyInfo == currentlyShown) return;

        currentlyShown = propertyInfo;
        deedParent.SetActive(true);
        estateDeed.gameObject.SetActive(false);
        railroadDeed.gameObject.SetActive(false);
        utilityDeed.gameObject.SetActive(false);
        tokenIcon.gameObject.SetActive(false);
        mortgagedText.SetActive(false);

        handleDeed();
        handleToken();
        handleMortgagedText();
    }
    private void clearShownCheck() {
        if (currentlyShown != null) {
            deedParent.SetActive(false);
            shownDeed.SetActive(false);
            tokenIcon.gameObject.SetActive(false);
            mortgagedText.SetActive(false);
            shownDeed = null;
            currentlyShown = null;
        }
    }
    #endregion
}
