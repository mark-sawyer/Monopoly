using UnityEngine;
using System.Linq;

public class TestPropertyObtaining : MonoBehaviour {
    #region Script references
    [SerializeField] private TestGameDataUpdater testGameDataUpdater;
    [SerializeField] private PlayerPanel playerPanel;
    [SerializeField] private MoneyChange moneyChange;
    [SerializeField] private PropertySelected propertySelected;
    [SerializeField] private PropertyGroupSelected propertyGroupSelected;
    #endregion
    #region PropertyGroups
    [SerializeField] private ScriptableObject[] estateGroupSOs;
    [SerializeField] private ScriptableObject[] railroadSOs;
    [SerializeField] private ScriptableObject[] utilitySOs;
    private EstateGroupInfo[] estateGroups;
    private RailroadInfo[] railroads;
    private UtilityInfo[] utilities;
    #endregion
    private PlayerInfo playerInfo;



    #region MonoBehaviour
    private void Awake() {
        GameFactory gameFactory = new GameFactory();
        gameFactory.makeGame(1);
        playerInfo = gameFactory.GameStateInfo.TurnPlayer;
        playerPanel.setup(playerInfo);
        estateGroups = estateGroupSOs.Cast<EstateGroupInfo>().ToArray();
        railroads = railroadSOs.Cast<RailroadInfo>().ToArray();
        utilities = utilitySOs.Cast<UtilityInfo>().ToArray();
        testGameDataUpdater.setGamePlayer(gameFactory.GamePlayer);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            DataEventHub.Instance.call_MoneyAdjustment(playerInfo, moneyChange.Amount);
        }
        else if (Input.GetKeyDown(KeyCode.Delete)) {
            DataEventHub.Instance.call_MoneyAdjustment(playerInfo, -moneyChange.Amount);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            PropertyInfo property = getProperty(propertyGroupSelected.PropertyGroup, propertySelected.ID);
            if (property == null) return;
            
            if (property.Owner == null && property.Cost <= playerInfo.Money) {
                DataEventHub.Instance.call_PlayerObtainedProperty(playerInfo, property);
                DataEventHub.Instance.call_MoneyAdjustment(playerInfo, -property.Cost);
            }
            else if (property is EstateInfo estateInfo && estateInfo.CanAddBuilding && estateInfo.BuildingCost <= playerInfo.Money) {
                DataEventHub.Instance.call_EstateAddedBuilding(estateInfo);
                DataEventHub.Instance.call_MoneyAdjustment(playerInfo, -estateInfo.BuildingCost);
            }
        }            
    }
    #endregion



    private PropertyInfo getProperty(int propertyGroup, int propertyNum) {
        PropertyInfo getEstate(EstateGroupInfo egi, int propertyNum) {
            int totalProperties = egi.NumberOfPropertiesInGroup;
            if (propertyNum > totalProperties) return null;
            else return egi.getEstateInfo(propertyNum - 1);
        }
        PropertyInfo getUtility(int propertyNum) {
            if (propertyNum > 2) return null;
            else return utilities[propertyNum - 1];
        }

        if (propertyGroup >= 1 && propertyGroup <= 8) return getEstate(estateGroups[propertyGroup - 1], propertyNum);
        if (propertyGroup == 9) return railroads[propertyNum - 1];
        if (propertyGroup == 10) return getUtility(propertyNum);

        throw new System.Exception("Invalid property group ID.");
    }
}
