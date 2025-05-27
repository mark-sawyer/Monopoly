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
    #region GameEvents
    [SerializeField] private GameEvent<PlayerInfo, int> moneyAdjustData;
    [SerializeField] private GameEvent<PlayerInfo> moneyAdjustUI;
    [SerializeField] private GameEvent<EstateInfo> estateAddedBuildingData;
    [SerializeField] private GameEvent<PlayerInfo, EstateInfo> estateAddedBuildingUI;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerObtainedPropertyData;
    [SerializeField] private GameEvent<PlayerInfo, PropertyInfo> playerPropertyAdjustmentUI;
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
            moneyAdjustData.invoke(playerInfo, moneyChange.Amount);
            moneyAdjustUI.invoke(playerInfo);
        }
        else if (Input.GetKeyDown(KeyCode.Delete)) {
            moneyAdjustData.invoke(playerInfo, -moneyChange.Amount);
            moneyAdjustUI.invoke(playerInfo);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            PropertyInfo property = getProperty(propertyGroupSelected.PropertyGroup, propertySelected.ID);
            if (property == null) return;
            
            if (property.Owner == null && property.Cost <= playerInfo.Money) {
                playerObtainedPropertyData.invoke(playerInfo, property);
                playerPropertyAdjustmentUI.invoke(playerInfo, property);
                moneyAdjustData.invoke(playerInfo, -property.Cost);
                moneyAdjustUI.invoke( playerInfo);
            }
            else if (property is EstateInfo estateInfo && estateInfo.canAddBuilding() && estateInfo.BuildingCost <= playerInfo.Money) {
                estateAddedBuildingData.invoke(estateInfo);
                playerPropertyAdjustmentUI.invoke(playerInfo, estateInfo);
                moneyAdjustData.invoke(playerInfo, -estateInfo.BuildingCost);
                moneyAdjustUI.invoke(playerInfo);
            }
        }            
    }
    #endregion



    private PropertyInfo getProperty(int propertyGroup, int propertyNum) {
        PropertyInfo getEstate(EstateGroupInfo egi, int propertyNum) {
            int totalProperties = egi.NumberOfEstatesInGroup;
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
