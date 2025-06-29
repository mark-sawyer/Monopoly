using UnityEngine;

public class QuickObtainProperty : MonoBehaviour {
    [SerializeField] private PropertyNumberSelect propertyNumberSelect;
    [SerializeField] private PropertyGroupSelect propertyGroupSelect;
    [SerializeField] private PlayerPropertyEvent playerObtainedProperty;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            int propertyNumber = propertyNumberSelect.PropertyNumber;
            int propertyGroupNumber = propertyGroupSelect.PropertyGroupNumber;
            
            if (propertyGroupNumber <= 8) {
                EstateGroupInfo estateGroupInfo = propertyGroupSelect.SelectedEstateGroup;
                int propertiesInGroup = estateGroupInfo.NumberOfEstatesInGroup;
                if (propertyNumber > propertiesInGroup) return;
                EstateInfo estateInfo = estateGroupInfo.getEstateInfo(propertyNumber - 1);
                if (estateInfo.IsBought) return;
                playerObtainedProperty.invoke(GameState.game.TurnPlayer, estateInfo);
            }
            else if (propertyGroupNumber == 9) {
                RailroadInfo railroadInfo = propertyGroupSelect.getRailroadInfo(propertyNumber - 1);
                if (railroadInfo.IsBought) return;
                playerObtainedProperty.invoke(GameState.game.TurnPlayer, railroadInfo);
            }
            else {
                if (propertyNumber > 2) return;
                UtilityInfo utilityInfo = propertyGroupSelect.getUtilityInfo(propertyNumber - 1);
                if (utilityInfo.IsBought) return;
                playerObtainedProperty.invoke(GameState.game.TurnPlayer, utilityInfo);
            }
        }
    }
}
