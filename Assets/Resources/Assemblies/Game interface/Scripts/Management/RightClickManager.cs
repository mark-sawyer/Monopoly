using UnityEngine;

public class RightClickManager : MonoBehaviour {
    [SerializeField] private EstateDeed estateDeed;
    [SerializeField] private RailroadDeed railroadDeed;
    [SerializeField] private UtilityDeed utilityDeed;
    [SerializeField] private TokenIcon tokenIcon;
    private bool inPreroll;
    private PropertyInfo currentlyShown;
    GameObject shownDeed;



    #region MonoBehaviour
    private void Start() {
        UIEventHub.Instance.sub_PrerollStateStarting(() => inPreroll = true);
        UIEventHub.Instance.sub_PrerollStateEnding(() => inPreroll = false);
    }
    private void Update() {
        if (!inPreroll) return;

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


        if (propertyInfo == currentlyShown) return;

        currentlyShown = propertyInfo;
        estateDeed.gameObject.SetActive(false);
        railroadDeed.gameObject.SetActive(false);
        utilityDeed.gameObject.SetActive(false);
        tokenIcon.gameObject.SetActive(false);

        handleDeed();
        handleToken();
    }
    private void clearShownCheck() {
        if (currentlyShown != null) {
            shownDeed.SetActive(false);
            tokenIcon.gameObject.SetActive(false);
            shownDeed = null;
            currentlyShown = null;
        }
    }
    #endregion
}
