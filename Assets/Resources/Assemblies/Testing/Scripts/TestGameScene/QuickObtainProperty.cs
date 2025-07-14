using UnityEngine;

public class QuickObtainProperty : MonoBehaviour {
    [SerializeField] private PropertyNumberSelect propertyNumberSelect;
    [SerializeField] private PropertyGroupSelect propertyGroupSelect;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            int propertyNumber = propertyNumberSelect.PropertyNumber;
            int propertyGroupNumber = propertyGroupSelect.PropertyGroupNumber;
            
            if (propertyGroupNumber <= 8) {
                EstateGroupInfo estateGroupInfo = propertyGroupSelect.SelectedEstateGroup;
                int propertiesInGroup = estateGroupInfo.NumberOfPropertiesInGroup;
                if (propertyNumber > propertiesInGroup) return;
                EstateInfo estateInfo = estateGroupInfo.getEstateInfo(propertyNumber - 1);
                if (estateInfo.IsBought) return;
                DataEventHub.Instance.call_PlayerObtainedProperty(GameState.game.TurnPlayer, estateInfo);
            }
            else if (propertyGroupNumber == 9) {
                RailroadInfo railroadInfo = propertyGroupSelect.getRailroadInfo(propertyNumber - 1);
                if (railroadInfo.IsBought) return;
                DataEventHub.Instance.call_PlayerObtainedProperty(GameState.game.TurnPlayer, railroadInfo);
            }
            else {
                if (propertyNumber > 2) return;
                UtilityInfo utilityInfo = propertyGroupSelect.getUtilityInfo(propertyNumber - 1);
                if (utilityInfo.IsBought) return;
                DataEventHub.Instance.call_PlayerObtainedProperty(GameState.game.TurnPlayer, utilityInfo);
            }
        }
    }
}
